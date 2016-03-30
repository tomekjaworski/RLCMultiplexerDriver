using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MultiplexerLib;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Text;
using System.Net;
using System.IO;

namespace MultiplexerGUI
{
    static class Program
    {
        public static Multiplexer m;
        public static Agilent a;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            a = new Agilent();
            m = new Multiplexer(false);

            // ustaw ustaw miejsce dla pliku

            bool force_new = false;
            if (String.IsNullOrEmpty(Properties.Settings.Default.meas_file_name))
                force_new = true;
            else
            {
                string path = Path.GetDirectoryName(Properties.Settings.Default.meas_file_name);
                if (!Directory.Exists(path))
                    force_new = true;
            }


            if (force_new)
            {
                string p = Path.GetDirectoryName(Application.ExecutablePath);
                Properties.Settings.Default.meas_file_name = Path.Combine(p, "pomiary.txt");
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            Properties.Settings.Default.Save();
        }
    }
}
