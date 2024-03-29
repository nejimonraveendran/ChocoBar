using System;
using System.Windows.Forms;

namespace ChocoBar
{
    internal static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var res = Win32.SetProcessDPIAware();

            if (!res)
            {
                MessageBox.Show("Failed to set the application DPI aware", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
