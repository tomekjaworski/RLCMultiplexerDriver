using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MultiplexerLib;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Text;
using System.Net;

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            Properties.Settings.Default.Save();
        }
    }
}
