using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ChocoBar.Win32;

namespace ChocoBar
{
    internal class AppBarInfo
    {
        public Win32.AppBarData AppBarData { get; set; }
    }

    internal static class AppBar
    {
        public static AppBarInfo RegisterAppBar(IntPtr hWnd, int thickness, Win32.AppBarEdge edge)
        {
            var appBarData = new Win32.AppBarData();
            appBarData.cbSize = Marshal.SizeOf(appBarData);
            appBarData.hWnd = hWnd;
            appBarData.uCallbackMessage = (uint)Win32.WindowMessages.APPBAR_CALLBACK_CUSTOM_MSG;

            var isSuccess = Win32.SHAppBarMessage(Win32.AppBarMessage.ABM_NEW, ref appBarData);
            if (isSuccess == IntPtr.Zero)
            {
                throw new Exception("Failed to register AppBar");
            }

            appBarData.uEdge = edge;

            switch (appBarData.uEdge)
            {
                case Win32.AppBarEdge.ABE_LEFT:
                    appBarData.rc.Left = Screen.PrimaryScreen.WorkingArea.Left;
                    appBarData.rc.Right = Screen.PrimaryScreen.WorkingArea.Left + thickness;
                    appBarData.rc.Top = Screen.PrimaryScreen.WorkingArea.Top;
                    appBarData.rc.Bottom = Screen.PrimaryScreen.WorkingArea.Height;
                    break;
                case Win32.AppBarEdge.ABE_RIGHT:
                    appBarData.rc.Left = Screen.PrimaryScreen.WorkingArea.Width - thickness;
                    appBarData.rc.Right = Screen.PrimaryScreen.WorkingArea.Width;
                    appBarData.rc.Top = Screen.PrimaryScreen.WorkingArea.Top;
                    appBarData.rc.Bottom = Screen.PrimaryScreen.WorkingArea.Height;
                    break;
                case Win32.AppBarEdge.ABE_TOP:
                    appBarData.rc.Left = Screen.PrimaryScreen.WorkingArea.Left;
                    appBarData.rc.Right = Screen.PrimaryScreen.WorkingArea.Width;
                    appBarData.rc.Top = Screen.PrimaryScreen.WorkingArea.Top;
                    appBarData.rc.Bottom = Screen.PrimaryScreen.WorkingArea.Top + thickness;
                    break;
                case Win32.AppBarEdge.ABE_BOTTOM:
                    //not implemented
                    break;
                default:
                    break;
            }

            isSuccess = Win32.SHAppBarMessage(Win32.AppBarMessage.ABM_QUERYPOS, ref appBarData);
            if (isSuccess == IntPtr.Zero)
            {
                throw new Exception("Failed to query AppBar position");
            }

            isSuccess = Win32.SHAppBarMessage(Win32.AppBarMessage.ABM_SETPOS, ref appBarData);

            if (isSuccess == IntPtr.Zero)
            {
                throw new Exception("Failed to set AppBar position");
            }

            return new AppBarInfo { AppBarData = appBarData };

        }

        public static bool UnRegisterAppBar(Form form)
        {
            var appBarData = new Win32.AppBarData();
            appBarData.cbSize = Marshal.SizeOf(appBarData);
            appBarData.hWnd = form.Handle;

            return Win32.SHAppBarMessage(AppBarMessage.ABM_REMOVE, ref appBarData) != IntPtr.Zero;
        }


    }
}
