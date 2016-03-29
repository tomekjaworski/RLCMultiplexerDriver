using MultiplexerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PomiarWilgotnosci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.UpdateGUI();
        }

        private void UpdateGUI()
        {
            this.btnStartStop.Text = this.timer1.Enabled ? "Stop (3)" : "Start (3)";
        }

        private void btnRLCConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Program.a.Connect(this.tbRLCAddress.Text, 10000);
                MessageBox.Show("Połączenie z mostkiem RLC nawiązane", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas łączenia z mostkiem RLC:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Program.m.SafeDisconnect();
            }
        }

        private void btnSendParametersToRLC_Click(object sender, EventArgs e)
        {

            int freq = (int)this.edtMeasurementFrequency.Value;
            double voltage = (double)this.edtVoltage.Value;
            double delay = (double)this.edtTriggerDelay.Value;
            int average = (int)this.edtAverage.Value;


            try
            {
                Program.a.SendConfiguration(MeasurementType.Resistance_Reactance, freq, voltage, delay, 0, average);
                MessageBox.Show("Konfiguracja wysłana do mostka", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas komunikacji z mostkiem:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.a.SafeDisconnect();
            }

        }

        private void btnDoOneMeasurement_Click(object sender, EventArgs e)
        {
            Complex imp = Program.a.GetMeasurement();
            this.label148.Text = AgilentHelper.ImpedanceToString(imp, false);
        }



        private void btnStartStop_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = (int)(this.numericUpDown1.Value * 1000);
            this.timer1.Enabled = !this.timer1.Enabled;
            this.UpdateGUI();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label148.Text = "Trwa pomiar...";
            Application.DoEvents();
            Thread.Sleep(0);

            Complex imp = Program.a.GetMeasurement();

            this.label148.Text = AgilentHelper.ImpedanceToString(imp, false);
            Application.DoEvents();
            Thread.Sleep(0);

            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongTimeString());
            sb.Append("\t");
            sb.Append(imp.Real);
            sb.Append("\t");
            sb.Append(imp.Imaginary);
            sb.AppendLine();

            using (StreamWriter sw = File.AppendText(this.tbFileName.Text))
            {
                sw.Write(sb.ToString());
                sw.Flush();
            }
        }
    }
}
