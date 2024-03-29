using System.Runtime.InteropServices;

namespace ChocoBar
{
    internal static class Input
    {
        const string cppLib = @"Input.dll";


        [DllImport(cppLib, EntryPoint = "SimulateKeyDown", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void SimulateKeyDown(int keyCode, bool isShift, bool isCtrl, bool isAlt);


        [DllImport(cppLib, EntryPoint = "SimulateKeyUp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void SimulateKeyUp(int keyCode, bool isShift, bool isCtrl, bool isAlt);



    }
}
