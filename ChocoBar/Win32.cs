using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChocoBar
{
    internal static class Win32
    {
        //enums
        internal enum GWL : int
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        internal enum HwndInsertAfter : int
        {
            HWND_BOTTOM = 1,
            HWND_NOTOPMOST = -2,
            HWND_TOP = 0,
            HWND_TOPMOST = -1
        }

        internal enum SetWindowPosFlags : uint
        {
            SWP_ASYNCWINDOWPOS = 0x4000,
            SWP_DEFERERASE = 0x2000,
            SWP_DRAWFRAME = 0x20,
            SWP_FRAMECHANGED = 0x20,
            SWP_HIDEWINDOW = 0x80,
            SWP_NOACTIVATE = 0x10,
            SWP_NOCOPYBITS = 0x100,
            SWP_NOMOVE = 0x2,
            SWP_NOOWNERZORDER = 0x200,
            SWP_NOREDRAW = 0x8,
            SWP_NOREPOSITION = 0x200,
            SWP_NOSENDCHANGING = 0x400,
            SWP_NOSIZE = 0x1,
            SWP_NOZORDER = 0x4,
            SWP_SHOWWINDOW = 0x40
        }

        [Flags]
        internal enum WindowStyles : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,

            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,

            WS_CAPTION = WS_BORDER | WS_DLGFRAME,
            WS_TILED = WS_OVERLAPPED,
            WS_ICONIC = WS_MINIMIZE,
            WS_SIZEBOX = WS_THICKFRAME,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,

            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_CHILDWINDOW = WS_CHILD,

            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,

            //#if(WINVER >= 0x0400)

            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,

            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,

            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
            //#endif /* WINVER >= 0x0400 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_LAYERED = 0x00080000,
            //#endif /* WIN32WINNT >= 0x0500 */

            //#if(WINVER >= 0x0500)

            WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
            WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring
                                          //#endif /* WINVER >= 0x0500 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000
            //#endif /* WIN32WINNT >= 0x0500 */

        }


        internal enum WindowMessages : uint
        {
            WM_USER = 0x0400,
            APPBAR_CALLBACK_CUSTOM_MSG = WM_USER + 1010,

            WM_POINTERENTER = 0x0249,
            WM_POINTERDOWN = 0x0246,
            WM_POINTERUP = 0x0247,
            WM_POINTERUPDATE = 0x0245,
            WM_POINTERLEAVE = 0x024A,

            WM_SETFOCUS = 0x0007,
            WM_DISPLAYCHANGE = 0x007E,

            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,

            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,

        }


        internal enum AppBarMessage : int
        {
            ABM_NEW = 0x00000000,
            ABM_REMOVE = 0x00000001,
            ABM_QUERYPOS = 0x00000002,
            ABM_SETPOS = 0x00000003,
            ABM_GETSTATE = 0x00000004,
            ABM_GETTASKBARPOS = 0x00000005,
            ABM_ACTIVATE = 0x00000006,
            ABM_GETAUTOHIDEBAR = 0x00000007,
            ABM_SETAUTOHIDEBAR = 0x00000008,
            ABM_WINDOWPOSCHANGED = 0x00000009,
            ABM_SETSTATE = 0x0000000A,
            ABM_GETAUTOHIDEBAREX = 0x0000000B,
            ABM_SETAUTOHIDEBAREX = 0x0000000C
        }

        internal enum AppBarNotification : int
        {
            ABN_STATECHANGE = 0x00000000,
            ABN_POSCHANGED = 0x00000001,
            ABN_FULLSCREENAPP = 0x00000002,
            ABN_WINDOWARRANGE = 0x00000003
        }


        internal enum AppBarEdge : uint
        {
            ABE_LEFT = 0,
            ABE_TOP = 1,
            ABE_RIGHT = 2,
            ABE_BOTTOM = 3
        }

        //structures
        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AppBarData
        {
            public int cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public AppBarEdge uEdge;
            public Rect rc;
            public IntPtr lParam; // message specific
        }

        [Flags]
        public enum InputType : int
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        public enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };


        public enum TernaryRasterOperations : uint
        {
            /// <summary>dest = source</summary>
            SRCCOPY = 0x00CC0020,
            /// <summary>dest = source OR dest</summary>
            SRCPAINT = 0x00EE0086,
            /// <summary>dest = source AND dest</summary>
            SRCAND = 0x008800C6,
            /// <summary>dest = source XOR dest</summary>
            SRCINVERT = 0x00660046,
            /// <summary>dest = source AND (NOT dest)</summary>
            SRCERASE = 0x00440328,
            /// <summary>dest = (NOT source)</summary>
            NOTSRCCOPY = 0x00330008,
            /// <summary>dest = (NOT src) AND (NOT dest)</summary>
            NOTSRCERASE = 0x001100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MERGECOPY = 0x00C000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = 0x00BB0226,
            /// <summary>dest = pattern</summary>
            PATCOPY = 0x00F00021,
            /// <summary>dest = DPSnoo</summary>
            PATPAINT = 0x00FB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PATINVERT = 0x005A0049,
            /// <summary>dest = (NOT dest)</summary>
            DSTINVERT = 0x00550009,
            /// <summary>dest = BLACK</summary>
            BLACKNESS = 0x00000042,
            /// <summary>dest = WHITE</summary>
            WHITENESS = 0x00FF0062,
            /// <summary>
            /// Capture window as seen on screen.  This includes layered windows
            /// such as WPF windows with AllowsTransparency="true"
            /// </summary>
            CAPTUREBLT = 0x40000000
        }




        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }


        //imports

        [DllImport("user32.dll", EntryPoint = "SetProcessDPIAware", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SetProcessDPIAware();

        [DllImport("user32.dll", EntryPoint = "SetWindowLongW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SetWindowLongW(IntPtr hWnd, GWL nIndex, WindowStyles dwNewLong);


        [DllImport("user32.dll", EntryPoint = "SetWindowPos", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SetWindowPos(IntPtr hWnd, HwndInsertAfter hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);


        [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SHAppBarMessage(AppBarMessage dwMessage, ref AppBarData pData);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        internal static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        internal static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);


        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);



        internal static int LoWord(int dwValue)
        {
            return dwValue & 0xFFFF;
        }

        internal static int HiWord(int dwValue)
        {
            return (dwValue >> 16) & 0xFFFF;
        }


        internal static bool IsPointerMessage(Message msg)
        {
            return msg.Msg == (int)WindowMessages.WM_POINTERENTER ||
                   msg.Msg == (int)WindowMessages.WM_POINTERDOWN ||
                   msg.Msg == (int)WindowMessages.WM_POINTERUP ||
                   msg.Msg == (int)WindowMessages.WM_POINTERLEAVE ||
                   msg.Msg == (int)WindowMessages.WM_POINTERUPDATE;
        }

        internal static int GetPointerId(Message msg)
        {
            return LoWord(msg.WParam.ToInt32());
        }

        internal static bool IsPointerInContact(Message msg)
        {
            if (msg.Msg != (int)WindowMessages.WM_POINTERUPDATE)
            {
                return false;
            }

            int POINTER_MESSAGE_FLAG_INCONTACT = 0x00000004;
            return (HiWord(msg.WParam.ToInt32()) & POINTER_MESSAGE_FLAG_INCONTACT) == POINTER_MESSAGE_FLAG_INCONTACT;

        }

        internal static bool IsPointerInRange(Message msg)
        {
            if (msg.Msg != (int)WindowMessages.WM_POINTERUPDATE)
            {
                return false;
            }

            int POINTER_MESSAGE_FLAG_INRANGE = 0x00000002;
            return (HiWord(msg.WParam.ToInt32()) & POINTER_MESSAGE_FLAG_INRANGE) == POINTER_MESSAGE_FLAG_INRANGE;

        }



    }
}
