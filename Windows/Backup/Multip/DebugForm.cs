using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using MultiplexerLib;

namespace MultiplexerGUI
{
    public partial class DebugForm : Form
    {

        public DebugForm()
        {
            this.InitializeComponent();
        }

        private void OnBitFieldClick(object sender, EventArgs e)
        {
            try
            {

                CheckBox chk = sender as CheckBox;
                string tag = chk.Tag as string;

                string nazwa = "???";
                int numer = 0;

                // wyciagniecie numeru karty
                while (char.IsDigit(tag[0]))
                {
                    numer *= 10;
                    numer += tag[0] - '0';
                    tag = tag.Remove(0, 1);
                }

                if (tag == "l")
                    tag = "LED";
                else
                    tag = tag.ToUpper();


                // metaprogramowanie jest zajebiste :D
                Program.m.SetSwitch(numer, (MultiplexerLib.Switches)Enum.Parse(typeof(Switches), tag), chk.Checked);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas wysyłania macierzy stanów:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.m.Connect(this.textBox1.Text);

                MessageBox.Show("Połączenie nawiązane", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas łączenia z multiplekserem:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.m.SafeDisconnect();
            }
        }

    }

}
