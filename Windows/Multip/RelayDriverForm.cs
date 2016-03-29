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
    public partial class RelayDriverForm : Form
    {

        public RelayDriverForm()
        {
            this.InitializeComponent();
        }

        private void OnBitFieldClick(object sender, EventArgs e)
        {
            try
            {

                CheckBox chk = sender as CheckBox;
                string tag = chk.Tag as string;
                // np "11cg" = karta 11, gniazdo C, ground

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
                Program.m.SetSwitch(numer, (MultiplexerLib.Switch)Enum.Parse(typeof(Switch), tag), chk.Checked);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas wysyłania macierzy stanów:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void RelayDriverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }

}
