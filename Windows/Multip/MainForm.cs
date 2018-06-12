using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MultiplexerLib;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Numerics;
using System.Diagnostics;

namespace MultiplexerGUI
{
    public partial class MainForm : Form
    {
        RelayDriverForm relay_driver_window;
        SocketDriverForm socket_driver_window;

        MeasurementGrid mg;

        public MainForm()
        {
            InitializeComponent();

            this.relay_driver_window = new RelayDriverForm();
            this.socket_driver_window = new SocketDriverForm();

            this.rbSerialCapacitance.Checked = Properties.Settings.Default.measurement_type == MeasurementType.Capacitance_Serial;
            this.rbParallelCapacitance.Checked = Properties.Settings.Default.measurement_type == MeasurementType.Capacitance_Parallel;
            this.rbImpedance.Checked = Properties.Settings.Default.measurement_type == MeasurementType.Resistance_Reactance;

            this.UpdateGUI();
        }
   

        private void btnConnectToMultiplexer_Click(object sender, EventArgs e)
        {
            try
            {
                Program.m.Connect(this.tbMultiplexerAddress.Text);
                this.tmrMultKeepAlive.Start();
                this.UpdateGUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas łączenia z multiplekserem:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.m.SafeDisconnect();
            }
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
                MessageBox.Show("Wyjątek podczas łączenia z multiplekserem:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.m.SafeDisconnect();
            }
        }


        private void btnShowRelayDriverWindow_Click(object sender, EventArgs e)
        {
            this.relay_driver_window.Show();
        }

        private void btnSendParametersToRLC_Click(object sender, EventArgs e)
        {
            MeasurementType mt = MeasurementType.Unknown;

            if (this.rbParallelCapacitance.Checked)
                mt = MeasurementType.Capacitance_Parallel;
            if (this.rbSerialCapacitance.Checked)
                mt = MeasurementType.Capacitance_Serial;
            if (this.rbImpedance.Checked)
                mt = MeasurementType.Resistance_Reactance;

            int freq = (int)this.edtMeasurementFrequency.Value;
            double voltage = (double)this.edtVoltage.Value;
            double delay = (double)this.edtTriggerDelay.Value;
            int average = (int)this.edtAverage.Value;

            Debug.Assert(mt != MeasurementType.Unknown);

            try
            {
                Program.a.SendConfiguration(mt, freq, voltage, delay, 0, average);
                MessageBox.Show("Konfiguracja wysłana do mostka.\nTryb pomiarowy=" + mt.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas komunikacji z mostkiem:\n" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.a.SafeDisconnect();
            }

        }

        private void edtNumberOfElectrodes_ValueChanged(object sender, EventArgs e)
        {
            int N = (int)this.edtNumberOfElectrodes.Value;
            int meas = (N * (N - 1)) / 2;
            this.lblNumberOfMeasurements.Text = meas.ToString();
        }

        private void edtNumberOfElectrodes_BindingContextChanged(object sender, EventArgs e)
        {
           this.edtNumberOfElectrodes_ValueChanged(sender, e);
        }

        private void btnCommenceMeasurements_Click(object sender, EventArgs e)
        {
            // pobierz nazwę pliku i uzupełnij ją o datę i czas
            string fname = Properties.Settings.Default.meas_file_name;
            string dt = DateTime.Now.ToString("HHmmss-ddMMyyyy");
            fname = Path.Combine(Path.GetDirectoryName(fname), Path.GetFileNameWithoutExtension(fname) + "-" + dt + Path.GetExtension(fname));



            if (Program.a.MeasurementType == MeasurementType.Capacitance_Parallel || Program.a.MeasurementType == MeasurementType.Capacitance_Serial)
            {
                this.DoCapacitanceMeasurement(sender == this.btnCommenceMeasurements);
                this.SaveCapacitanceData(fname);
            }
            else
            {
                this.DoImpedanceMeasurement(sender == this.btnCommenceMeasurements);
                this.SaveImpedanceData(fname);
            }



        }


        private void DoCapacitanceMeasurement(bool show_finish_message)
        {
            DateTime tstart = DateTime.Now;

            int N = (int)this.edtNumberOfElectrodes.Value;
            int pairs = (N * (N - 1)) / 2;
            int measurements_per_pair = (int)this.edtNumberOfMeasurementsPerPair.Value;

            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = pairs * measurements_per_pair;

            if (mg == null)
                mg = new MeasurementGrid();

            DataTable dt = mg.PrepareDataContainer(N, Program.a.MeasurementType);
            mg.Show();

            Program.m.SetAllChannels(ChannelState.Ground);
            Thread.Sleep(200);

            Program.a.RestartMeasurementCycle();

            for (int excitated_electrode = 1; excitated_electrode <= N; excitated_electrode++)
            {
                // ustaw elektrodę wymuszającą na wysokim wejsciu mostka
                Program.m.SetChannel(excitated_electrode, ChannelState.High);
                Thread.Sleep(300);
                Program.a.GetMeasurement();

                for (int measured_electrode = excitated_electrode + 1; measured_electrode <= N; measured_electrode++)
                {
                    // ustaw elektrode mierzona na niskim wejsciu mostka
                    Program.m.SetChannel(measured_electrode, ChannelState.Low);
                    Program.a.ShowMessage(string.Format("Elektrody {0}-{1}", excitated_electrode, measured_electrode));

                    // wykonaj serię pomiaraów dla danej kombinacji elektrod
                    for (int i = 0; i < measurements_per_pair; i++)
                    {
                        Application.DoEvents();
                        if (this.socket_driver_window != null)
                            this.socket_driver_window.UpdateGUI();
                        Application.DoEvents();
                        Complex cap = Program.a.GetMeasurement();
                        Application.DoEvents();


                        dt.Rows[excitated_electrode - 1][measured_electrode - 1] = cap;
                        //pom.Add(cap.Real);

                        this.progressBar1.Value++;

                        this.lblCurrentCapacity.Text = AgilentHelper.CapacitanceToString(cap, false);

                        Application.DoEvents();
                    }

                    Program.m.SetChannel(measured_electrode, ChannelState.Ground);
                    Application.DoEvents();

                }

                Program.m.SetChannel(excitated_electrode, ChannelState.Ground);
                //Program.a.MeasureFrequency();
                //Application.DoEvents(); 
            }

            DateTime tstop = DateTime.Now;
            if (show_finish_message)
                MessageBox.Show(
                   string.Format("Pomiar zakończony.\nWykonano {0} pomiarów dla {1} par.\nCzas pomiaru: {2:N1} sekund.", measurements_per_pair * pairs, pairs, (tstop - tstart).TotalSeconds),
                   Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DoImpedanceMeasurement(bool show_finish_message)
        {
            DateTime tstart = DateTime.Now;

            int N = (int)this.edtNumberOfElectrodes.Value;
            int pairs = (N * (N - 1)) / 2;
            int measurements_per_pair = (int)this.edtNumberOfMeasurementsPerPair.Value;

            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = pairs * measurements_per_pair;

            if (mg == null)
                mg = new MeasurementGrid();

            DataTable dt = mg.PrepareDataContainer(N, Program.a.MeasurementType);
            mg.Show();

            Program.m.SetAllChannels(ChannelState.Ground);
            Thread.Sleep(200);

            Program.a.RestartMeasurementCycle();
            int prev_channel = 1;
            for (int excitated_electrode = 1; excitated_electrode <= N; excitated_electrode++)
            {
                // ustaw elektrodę wymuszającą na wysokim wejsciu mostka
                Program.m.SetChannel(excitated_electrode, ChannelState.High);
                Thread.Sleep(300);
                Program.a.GetMeasurement();

                for (int measured_electrode = excitated_electrode + 1; measured_electrode <= N; measured_electrode++)
                {
                    // ustaw elektrode mierzona na niskim wejsciu mostka
                    Program.m.SetChannel(prev_channel, ChannelState.Ground, measured_electrode, ChannelState.Low);
                    prev_channel = measured_electrode;
                    //Program.m.SetChannel(measured_electrode, ChannelState.Low);

                    Program.a.ShowMessage(string.Format("Elektrody {0}-{1}", excitated_electrode, measured_electrode));

                    // wykonaj serię pomiaraów dla danej kombinacji elektrod
                    for (int i = 0; i < measurements_per_pair; i++)
                    {
                        Application.DoEvents();

                        if (this.socket_driver_window != null)
                            this.socket_driver_window.UpdateGUI();
                        Application.DoEvents();

                        Complex cap = Program.a.GetMeasurement();
                        Application.DoEvents();

                        dt.Rows[excitated_electrode - 1][measured_electrode - 1] = cap;

                        this.lblCurrentCapacity.Text = AgilentHelper.ImpedanceToString(cap, false);
                        this.progressBar1.Value++;
                        Application.DoEvents();
                    }

                    //                    Program.m.SetChannel(measured_electrode, ChannelState.Ground);
                    // Application.DoEvents();

                }

                Program.m.SetChannel(excitated_electrode, ChannelState.Ground);
                //Program.a.MeasureFrequency();
                //Application.DoEvents(); 
            }

            DateTime tstop = DateTime.Now;


            if (show_finish_message)
                MessageBox.Show(
                   string.Format("Pomiar zakończony.\nWykonano {0} pomiarów dla {1} par.\nCzas pomiaru: {2:N1} sekund.", measurements_per_pair * pairs, pairs, (tstop - tstart).TotalSeconds),
                   Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnSaveMeasurements_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.meas_file_name);
            this.saveFileDialog1.FileName = Path.GetFileName(Properties.Settings.Default.meas_file_name);

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            Properties.Settings.Default.meas_file_name = saveFileDialog1.FileName;
        }

        private void SaveCapacitanceData(string file_name)
        {
            try
            {
                DataTable dt = this.mg.data;

                string fn = Path.GetFileNameWithoutExtension(file_name);
                string fn1 = fn + "-marix" + Path.GetExtension(file_name);
                fn1 = Path.Combine(Path.GetDirectoryName(file_name), fn1);

                // zapis macierzy pomiarów (ostatnich pomiarow)
                using (StreamWriter sw = new StreamWriter(new FileStream(fn1, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
                {
                    sw.WriteLine("{0} {1}", dt.Rows.Count, dt.Columns.Count);
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            Complex val = (Complex)dt.Rows[r][c];
                            string s = string.Format("{0} ", val.Real).Replace(',', '.');
                            sw.Write(s);
                        }
                        sw.WriteLine();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas zapisu danych: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveImpedanceData(string file_name)
        {
            try
            {
                DataTable dt = this.mg.data;
                string fn = Path.GetFileNameWithoutExtension(file_name);
                string fn1 = fn + "-marix" + Path.GetExtension(file_name);
                fn1 = Path.Combine(Path.GetDirectoryName(file_name), fn1);

                // zapis macierzy pomiarów (ostatnich pomiarow)
                using (StreamWriter sw = new StreamWriter(new FileStream(fn1, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
                {
                    sw.WriteLine("{0} {1}", dt.Rows.Count, dt.Columns.Count);
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            Complex val = (Complex)dt.Rows[r][c];

                            string s = string.Format("({0}; {1}) ", val.Real, val.Imaginary).Replace(',', '.');
                            sw.Write(s);
                        }
                        sw.WriteLine();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyjątek podczas zapisu danych: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoOneMeasurement_Click(object sender, EventArgs e)
        {
            Complex cap = Program.a.GetMeasurement();
            if (this.rbParallelCapacitance.Checked || this.rbSerialCapacitance.Checked)
                this.label148.Text = AgilentHelper.CapacitanceToString(cap, false);
            else if (this.rbImpedance.Checked)
                this.label148.Text = AgilentHelper.ImpedanceToString(cap, false);
            else
                this.label148.Text = "????";

        }

        private void btnSearchForMultiplexer_Click(object sender, EventArgs e)
        {
            this.label2.Text = "Wykrywanie...";
            this.label2.Font = new Font(label2.Font, FontStyle.Regular);
            this.label2.ForeColor = Color.Black;
            this.tbMultiplexerAddress.Text = "0.0.0.0";
            Application.DoEvents();
            Thread.Sleep(200);

            this.Cursor = Cursors.WaitCursor;

            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                s.EnableBroadcast = true;
                s.SendTo(Encoding.ASCII.GetBytes("Halo! Multiplekserku!"), new IPEndPoint(IPAddress.Broadcast, 14005));
                s.ReceiveTimeout = 5000;

                byte[] buffer = new byte[1024];
                EndPoint ep = new IPEndPoint(IPAddress.None, 0);
                int recv = s.ReceiveFrom(buffer, ref ep);
                String response = Encoding.ASCII.GetString(buffer, 0, recv);

                IPAddress addr = IPAddress.None;
                if (response == "Czego?? Grzecznie sie pytam.")
                    addr = (ep as IPEndPoint).Address;

                this.tbMultiplexerAddress.Text = addr.ToString();


                //MessageBox.Show("Wyszukiwanie multipleksera zakończyło się niezwykłym powodzeniem.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.label2.Font = new Font(label2.Font, FontStyle.Bold);
                this.label2.ForeColor = Color.DarkGreen;
                this.label2.Text = "Wykryto.";

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Błąd podczas wykrywania multipleksera: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.label2.Font = new Font(label2.Font, FontStyle.Bold);
                this.label2.ForeColor = Color.Red;
                this.label2.Text = "Bład!";

            }

            this.Cursor = Cursors.Default;
        }


        private void btnShowSocketDriverWindow_Click(object sender, EventArgs e)
        {
            this.socket_driver_window.Show();
        }

        private void tmrMultKeepAlive_Tick(object sender, EventArgs e)
        {
            Program.m.Ping();

            if (!Program.m.Connected)
            {
                this.tmrMultKeepAlive.Stop();
                this.UpdateGUI();
                return;
            }

            this.UpdateGUI();
        }

        private void UpdateGUI()
        {
            if (Program.m.Connected)
            {
                this.lblStatus.Text = "Połączony";
                this.lblStatus.ForeColor = Color.DarkGreen;
            } else
            {
                this.lblStatus.Text = "Brak połączenia";
                this.lblStatus.ForeColor = Color.Red;
            }

            this.btnStartStop.Text = this.timer1.Enabled ? "Stop (3)" : "Start (3)";

        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = (int)(this.numericUpDown1.Value * 1000);
            this.timer1.Enabled = !this.timer1.Enabled;
            this.UpdateGUI();

            if (this.timer1.Enabled)
                this.lblCurrentCapacity.Text = "Oczekiwanie na timer...";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.btnCommenceMeasurements_Click(sender, e);
            this.lblCurrentCapacity.Text = "Oczekiwanie na timer...";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.measurement_type = MeasurementType.Unknown;
            if (this.rbSerialCapacitance.Checked)
                Properties.Settings.Default.measurement_type = MeasurementType.Capacitance_Serial;
            if (this.rbParallelCapacitance.Checked)
                Properties.Settings.Default.measurement_type = MeasurementType.Capacitance_Parallel;
            if (this.rbImpedance.Checked)
                Properties.Settings.Default.measurement_type = MeasurementType.Resistance_Reactance;
        }
    }
}
