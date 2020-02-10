using FileManagerWithProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerWithProfiles
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            Application.Run(new AutentificationForm());

            if (Properties.Settings.Default.userName != null)
            {
                Application.Run(new MainForm());
            }

            Properties.Settings.Default.userName = null;
        }
    }
}
