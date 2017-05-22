using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SinoSunIDE
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEventHandler);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
            {
                Application.Run(new frmMAIN(args[0]));
            }
            else
            {
                Application.Run(new frmMAIN(null));
            }
        }

        static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"testme.log",
                      System.IO.FileMode.Append, System.IO.FileAccess.Write))
                {
                    using (System.IO.StreamWriter w = new System.IO.StreamWriter(fs,
                          System.Text.Encoding.UTF8))
                    {
                        w.WriteLine(e.ExceptionObject);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
