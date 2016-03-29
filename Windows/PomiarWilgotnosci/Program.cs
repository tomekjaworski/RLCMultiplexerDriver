using MultiplexerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PomiarWilgotnosci
{
    static class Program
    {
        public static Agilent a;


        [STAThread]
        static void Main()
        {
            a = new Agilent();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            Properties.Settings.Default.Save();
        }
    }
}
