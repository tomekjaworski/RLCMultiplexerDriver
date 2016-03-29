using MultiplexerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiplexerGUI
{
    public partial class SocketDriverForm : Form
    {
        public SocketDriverForm()
        {
            InitializeComponent();

            //

            foreach (Control ctrl in this.Controls)
            {
                if (!(ctrl is Label))
                    continue;

                Label lab = ctrl as Label;
                if (lab.Tag == null)
                    continue;

                if (lab.Tag.ToString() == "ABCD")
                {
                    lab.Tag = lab.Text;

                    if ((lab.Tag as string).Contains('L'))
                        lab.Text = lab.Text.Replace("L", "");
                    else
                        lab.Text = "-";
                }

            }
            this.UpdateGUI();
        }

        private void lblSocket_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Label label = sender as Label;
                string tag = label.Tag as string;

                string nazwa = "???";
                int numer = 0;

                // wyciagniecie numeru karty
                while (char.IsDigit(tag[0]))
                {
                    numer *= 10;
                    numer += tag[0] - '0';
                    tag = tag.Remove(0, 1);
                }

                if (tag == "A") numer = numer + 16 * 0;
                if (tag == "B") numer = numer + 16 * 1;
                if (tag == "C") numer = numer + 16 * 2;
                if (tag == "D") numer = numer + 16 * 3;

                if (tag == "L")
                {
                    bool led = !Program.m.GetLED(numer - 1);
                    //  label.BackColor = led ? Color.Red : Color.FromArgb(255,192,192);
                    Program.m.SetLED(numer - 1, led);
                    this.UpdateGUI();
                    return;
                }

                ChannelState state = Program.m.GetChannel(numer);

                // ustawienie nastepnego
                if (state == ChannelState.High)
                    state = ChannelState.Low;
                else
                    if (state == ChannelState.Low)
                    state = ChannelState.Ground;
                else
                        if (state == ChannelState.Ground)
                    state = ChannelState.None;
                else
                    state = ChannelState.High;


                Program.m.SetChannel(numer, state);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas wysyłania macierzy stanów:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            this.UpdateGUI();
        }

        public void UpdateGUI()
        {
            foreach (Control c in this.Controls)
            {
                if (!(c is Label))
                    continue;

                Label label = c as Label;
                string nazwa = "???";
                int numer = 0;
                string tag = label.Tag as string;
                if (string.IsNullOrEmpty(tag))
                    continue;

                // wyciagniecie numeru karty
                while (char.IsDigit(tag[0]))
                {
                    numer *= 10;
                    numer += tag[0] - '0';
                    tag = tag.Remove(0, 1);
                }

                if (tag.Length != 1)
                    continue;

                if (tag == "A") numer = numer + 16 * 0;
                if (tag == "B") numer = numer + 16 * 1;
                if (tag == "C") numer = numer + 16 * 2;
                if (tag == "D") numer = numer + 16 * 3;

                if (tag == "L")
                {
                    bool led = Program.m.GetLED(numer - 1);
                    label.BackColor = led ? Color.Red : Color.FromArgb(255, 192, 192);
                    continue;
                }


                ChannelState state = Program.m.GetChannel(numer);

                if (state == ChannelState.High)
                {
                    label.Text = "Hi";
                    label.ForeColor = Color.Red;
                }

                if (state == ChannelState.Low)
                {
                    label.Text = "Lo";
                    label.ForeColor = Color.Blue;
                }

                if (state == ChannelState.Ground)
                {
                    label.Text = "gnd";
                    label.ForeColor = Color.Black;
                }

                if (state == ChannelState.None)
                {
                    label.Text = "-";
                    label.ForeColor = Color.DarkGray;
                }

            }
        }

        private void OnSetAllToState_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender == btnGroundAll) Program.m.SetAllChannels(ChannelState.Ground);
                if (sender == btnLowAll) Program.m.SetAllChannels(ChannelState.Low);
                if (sender == btnHighAll) Program.m.SetAllChannels(ChannelState.High);
                if (sender == btnOffAll) Program.m.SetAllChannels(ChannelState.None);
                if (sender == btnAllLEDOff) Program.m.SetAllLED(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas wysyłania macierzy stanów:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.UpdateGUI();
        }

        private void SocketDriverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

        }
    }
}
