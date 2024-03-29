using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChocoBar
{
    internal static class WindowShot
    {
        static System.Timers.Timer _timer;
        static Win32.Rect _targetWindowRectangle = new Win32.Rect();
        static IntPtr _targetWindowHandle = IntPtr.Zero;
        static VideoFileWriter _videoWriter;
        static string _curVideoFolder = null;
        
        public static void StartRecording(string processName, string folderPath)
        {
            if (_videoWriter != null && _videoWriter.IsOpen)
            {
                throw new InvalidOperationException($"Another recording already in progress!");
            }

            //start recording
            _curVideoFolder = $@"{folderPath}\{processName}";

            //if the folder does not exist, create it
            if (!Directory.Exists(_curVideoFolder))
            {
                Directory.CreateDirectory(_curVideoFolder);
            }

            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
            {
                throw new InvalidOperationException($"The application {processName} is not running!");
            }

            _targetWindowHandle = process.MainWindowHandle;

            if(_targetWindowHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Failed to get the handle of the main window of the application {processName}!");
            }

            Win32.ShowWindow(_targetWindowHandle, Win32.ShowWindowEnum.ShowMaximized);
            Win32.SetForegroundWindow(_targetWindowHandle);

            var displayScalingFactor = getDisplayScalingFactor(_targetWindowHandle);

            Win32.GetClientRect(_targetWindowHandle, out _targetWindowRectangle);

            _targetWindowRectangle.Left = Convert.ToInt32(_targetWindowRectangle.Left * displayScalingFactor);
            _targetWindowRectangle.Right = Convert.ToInt32(_targetWindowRectangle.Right * displayScalingFactor);
            _targetWindowRectangle.Top = Convert.ToInt32(_targetWindowRectangle.Top * displayScalingFactor);
            _targetWindowRectangle.Bottom = Convert.ToInt32(_targetWindowRectangle.Bottom * displayScalingFactor);


            if (_targetWindowRectangle.Right <= 0 || _targetWindowRectangle.Bottom <= 0)
            {
                throw new InvalidOperationException($"Unable to determine the bounds of the main window of the application {processName}!");
            }

            //FFMPEG does not support odd number dimensions, so round to next even number
            _targetWindowRectangle.Left = _targetWindowRectangle.Left % 2 == 0 ? _targetWindowRectangle.Left : _targetWindowRectangle.Left + 1;
            _targetWindowRectangle.Right = _targetWindowRectangle.Right % 2 == 0 ? _targetWindowRectangle.Right : _targetWindowRectangle.Right + 1;
            _targetWindowRectangle.Top = _targetWindowRectangle.Top % 2 == 0 ? _targetWindowRectangle.Top : _targetWindowRectangle.Top + 1;
            _targetWindowRectangle.Bottom = _targetWindowRectangle.Bottom % 2 == 0 ? _targetWindowRectangle.Bottom : _targetWindowRectangle.Bottom + 1;

            _videoWriter = new VideoFileWriter();


            if (_timer == null)
            {
                _timer = new System.Timers.Timer(100);
                _timer.Elapsed += _timer_Elapsed;
            }

            _timer.Enabled = true;

        }


        public static void StopRecording()
        {
            if (_timer != null && _timer.Enabled)
            {
                _timer.Enabled = false; //first stop the timer   
            }

            if (_videoWriter != null && _videoWriter.IsOpen)
            {
                _videoWriter.Close();
                //_videoWriter.Dispose();
            }

            _videoWriter = null;

        }


        private static void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            using (var bitmap = captureWindowUsingBitBlt(false))
            {
                if (_videoWriter != null && !_videoWriter.IsOpen)
                {
                    _videoWriter.Open($@"{_curVideoFolder}\Capture_{DateTime.Now.ToString("MM.dd.yyyy.HH.mm.ss")}.avi",
                        _targetWindowRectangle.Right, _targetWindowRectangle.Bottom, 30, VideoCodec.MPEG4, 20000000);

                    //write first frame.  
                    _videoWriter.WriteVideoFrame(bitmap);
                    _videoWriter.Flush();

                }
                else
                {
                    if (isPossibleBlackScreen(bitmap)) //if a tooltip etc is causing black screen.
                        return;

                    _videoWriter.WriteVideoFrame(bitmap);
                    _videoWriter.Flush();
                    
                }
            }

        }

        private static bool isPossibleBlackScreen(Bitmap bitmap)
        {
            var pixel1 = bitmap.GetPixel(100, 100);
            var pixel2 = bitmap.GetPixel(1000, 100);
            var pixel3 = bitmap.GetPixel(100, 1000);
            var pixel4 = bitmap.GetPixel(1000, 1000);

            return isBlack(pixel1) && isBlack(pixel2) && isBlack(pixel3) && isBlack(pixel4);

        }


        private static bool isBlack(Color pixel)
        {
            return (pixel.R == 0 && pixel.G == 0 && pixel.B == 0);
        }


        private static Bitmap captureWindowUsingBitBlt(bool captureTitlebar)
        {
            IntPtr hWndDc = Win32.GetDC(_targetWindowHandle);
            IntPtr hMemDc = Win32.CreateCompatibleDC(hWndDc);
            IntPtr hBitmap = Win32.CreateCompatibleBitmap(hWndDc, _targetWindowRectangle.Right, _targetWindowRectangle.Bottom);
            Win32.SelectObject(hMemDc, hBitmap);

            Win32.BitBlt(hMemDc, 0, 0, _targetWindowRectangle.Right, _targetWindowRectangle.Bottom, hWndDc, 0, 0, Win32.TernaryRasterOperations.SRCCOPY | Win32.TernaryRasterOperations.CAPTUREBLT);
            var bitmap = Bitmap.FromHbitmap(hBitmap);

            Win32.DeleteObject(hBitmap);
            Win32.ReleaseDC(_targetWindowHandle, hWndDc);
            Win32.DeleteDC(hMemDc);
            Win32.DeleteDC(hWndDc);

            return bitmap;
        }


        private static Bitmap CaptureWindowUsingPrintWindow(bool captureTitlebar)
        {
            var bitmap = new Bitmap(_targetWindowRectangle.Right, _targetWindowRectangle.Bottom);

            // Use PrintWindow to draw the window into our bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                IntPtr hdc = g.GetHdc();
                if (captureTitlebar)
                    Win32.PrintWindow(_targetWindowHandle, hdc, 0);
                else
                    Win32.PrintWindow(_targetWindowHandle, hdc, 1);

                g.ReleaseHdc(hdc);
            }

            return bitmap;
        }


        private static decimal getDisplayScalingFactor(IntPtr hwnd)
        {
            var screen = Screen.FromHandle(hwnd);

            var dm = new Win32.DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(typeof(Win32.DEVMODE));

            Win32.EnumDisplaySettings(screen.DeviceName, -1, ref dm);

            var scalingFactor = Math.Round(Decimal.Divide(dm.dmPelsWidth, screen.Bounds.Width), 2);

            return scalingFactor;
        }

    }
}
