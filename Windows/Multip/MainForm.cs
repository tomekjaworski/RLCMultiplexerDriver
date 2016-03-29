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

namespace MultiplexerGUI
{
    public partial class MainForm : Form
    {
        RelayDriverForm relay_driver_window;
        SocketDriverForm socket_driver_window;

        MeasurementGrid mg;
        List<List<double>> full_sensor_measurements;

        public MainForm()
        {
            InitializeComponent();

            this.relay_driver_window = new RelayDriverForm();
            this.socket_driver_window = new SocketDriverForm();

        }

    

        private void btnConnectToMultiplexer_Click(object sender, EventArgs e)
        {
            try
            {
                Program.m.Connect(this.tbMultiplexerAddress.Text);
                this.tmrMultKeepAlive.Start();
                this.UpdateGUI();
                //MessageBox.Show("Połączenie z multiplexerem nawiązane", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //if (relay_driver_window == null)
            //{
              //  this.relay_driver_window = new RelayDriverForm();
                this.relay_driver_window.Show();
            //}
        }

        private void btnSendParametersToRLC_Click(object sender, EventArgs e)
        {
            if (this.rbParallelImpedance.Checked && this.rbSerialImpedance.Checked || !this.rbParallelImpedance.Checked && !this.rbSerialImpedance.Checked)
                this.rbParallelImpedance.Checked = true;


            bool para = this.rbParallelImpedance.Checked;
            int freq = (int)this.edtMeasurementFrequency.Value;
            double voltage = (double)this.edtVoltage.Value;
            double delay = (double)this.edtTriggerDelay.Value;
            int average = (int)this.edtAverage.Value;


            try
            {
                Program.a.SendConfiguration(para ? MeasurementType.Capacitance_Parallel : MeasurementType.Capacitance_Serial, freq, voltage, delay, 0,average);
                MessageBox.Show("Konfiguracja wysłana do mostka", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
           /* float[,] intrinsic_cap = null;
            DialogResult dr = MessageBox.Show("Czy chcesz uwzględnić macierz pojemności własnych multipleksera w rozpoczynanym pomiarze czujnika ECT?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
                return; // e.. wlasciwie to mi sie nie chce wykonywac pomiarów... ;-)
            if (dr == DialogResult.Yes)
            {
                try
                {
                    intrinsic_cap = this.TryLoadIntrinsicCapacitances(Properties.Settings.Default.cap_offset_file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas wczytywania pliku pojemności własnych:\n " + ex.Message+"\n\nProces pomiarowy został przerwany.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }*/

            int N = (int)this.edtNumberOfElectrodes.Value;
            int pairs = (N * (N - 1)) / 2;
            int measurements_per_pair = (int)this.edtNumberOfMeasurementsPerPair.Value;

            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = pairs * measurements_per_pair;

            if (mg == null)
                mg = new MeasurementGrid();

            DataTable dt = mg.PrepareDataContainer(N);
            mg.Show();

            this.full_sensor_measurements = new List<List<double>>();
            this.full_sensor_measurements.Add(new List<double>(new double[] { N, pairs, measurements_per_pair }));

            Program.m.SetAllChannels(ChannelState.Ground);
            Thread.Sleep(200);

            Program.a.RestartMeasurementCycle();

            for (int excitated_electrode = 1; excitated_electrode <= N; excitated_electrode++)
            {
                // ustaw elektrodę wymuszającą na wysokim wejsciu mostka
                Program.m.SetChannel(excitated_electrode, ChannelState.High);
                Thread.Sleep(300);
                Program.a.MeasureFrequency();

                for (int measured_electrode = excitated_electrode + 1; measured_electrode <= N; measured_electrode++)
                {
                    // ustaw elektrode mierzona na niskim wejsciu mostka
                    Program.m.SetChannel(measured_electrode, ChannelState.Low);
                    Program.a.ShowMessage(string.Format("Elektrody {0}-{1}", excitated_electrode, measured_electrode));

                    // nowy wektro pomiarowy
                    List<double> pom = new List<double>();
                    pom.AddRange(new double[] { excitated_electrode, measured_electrode, measurements_per_pair });

                    // wykonaj serię pomiaraów dla danej kombinacji elektrod
                    for (int i = 0; i < measurements_per_pair; i++)
                    {
                        Application.DoEvents();
                        if (this.socket_driver_window != null)
                            this.socket_driver_window.UpdateGUI();
                        Application.DoEvents();
                        double c = Program.a.MeasureFrequency();
                        Application.DoEvents();

                        // korekta
                       // if (intrinsic_cap != null)
                        //    c = c - intrinsic_cap[excitated_electrode - 1, measured_electrode - 1];

                        dt.Rows[excitated_electrode - 1][measured_electrode - 1] = c;
                        pom.Add(c);

                        this.progressBar1.Value++;
                        this.lblCurrentCapacity.Text = FormatSmallValue(c, "N5");
                        Application.DoEvents();
                    }
                    full_sensor_measurements.Add(pom);

                    Program.m.SetChannel(measured_electrode, ChannelState.Ground);
                    Application.DoEvents();

                }

                Program.m.SetChannel(excitated_electrode, ChannelState.Ground);
                //Program.a.MeasureFrequency();
                //Application.DoEvents(); 
            }

            MessageBox.Show(
                string.Format("Pomiar zakończony.\nWykonano {0} pomiarów dla {1} par", measurements_per_pair * pairs, pairs),
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

  
        private void btnSaveMeasurements_Click(object sender, EventArgs e)
        {
            if (mg == null)
                return;

            try
            {
                DataTable dt = this.mg.data;

                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return;

                string path = saveFileDialog1.FileName;
                string fn = Path.GetFileNameWithoutExtension(path);
                string fn1 = fn + "-marix" + Path.GetExtension(path);
                string fn2 = fn + "-list" + Path.GetExtension(path); ;
                fn1 = Path.Combine(Path.GetDirectoryName(path), fn1);
                fn2 = Path.Combine(Path.GetDirectoryName(path), fn2);

                // zapis macierzy pomiarów (ostatnich pomiarow)
                using (StreamWriter sw = new StreamWriter(new FileStream(fn1, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
                {
                    sw.WriteLine("{0} {1}", dt.Rows.Count, dt.Columns.Count);
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            double val = (double)dt.Rows[r][c];
                            string s = string.Format("{0} ", val);
                            sw.Write(s.Replace(',', '.'));
                        }
                        sw.WriteLine();
                    }
                }

                // zapis listy pomiarów (syćkich)
                using (StreamWriter sw = new StreamWriter(new FileStream(fn2, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
                {
                    for (int i = 0; i < full_sensor_measurements.Count; i++)
                    {
                        List<double> pom = this.full_sensor_measurements[i];

                        if (i == 0)
                        {
                            // naglowek
                            sw.WriteLine("{0} {1} {2}", (int)pom[0], (int)pom[1], (int)pom[2]);
                            continue;
                        }

                        sw.Write("{0} {1} {2} ", (int)pom[0], (int)pom[1], (int)pom[2]);
                        for (int j = 3; j < pom.Count; j++)
                        {
                            string s = string.Format("{0} ", pom[j]);
                            sw.Write(s.Replace(',', '.'));

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
            double c = Program.a.MeasureFrequency();
            this.label148.Text = FormatSmallValue(c, "N2");
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


        ///

        static string FormatSmallValue(double c, string fmt)
        {
            int pow = 0;

            string view = "";
            if (c > 100)
                view = "overload";
            else
            {
                // przyrostek (10e-3=m, 10e-6=u, itd...)
                string[] s = { "", "m", "u", "n", "p", "f", "a" };

                while ((int)c == 0)
                {
                    pow++;
                    c = c * Math.Pow(10, 3);
                }
                view = c.ToString(fmt) + s[pow] + "F";
            }

            return view;
        }

        private void btnSelectIntrinsicCaps_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            Properties.Settings.Default.cap_offset_file = null;

            try
            {
                float[,] intrinsic_cap = this.TryLoadIntrinsicCapacitances(this.openFileDialog1.FileName);
                Properties.Settings.Default.cap_offset_file = this.openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania pliku pojemności własnych: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Wczytaj plik z pojemnościami własnymi poszczególnych torów pomiarowych multipleksera
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        private float[,] TryLoadIntrinsicCapacitances(string file_name)
        {
            //4 6 1
            //1 2 15.78992E-12 
            //1 3 15.79526E-12 
            //1 4 15.79985E-12 
            //2 3 15.79061E-12 
            //2 4 15.79731E-12 
            //3 4 15.80734E-12 

            StreamReader sr = new StreamReader(new FileStream(file_name, FileMode.Open, FileAccess.Read, FileShare.Read));
            string[] str_numbers = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (str_numbers.Length != 3)
                throw new FormatException("Błąd w nagłówku pliku - błąd nagłówka");

            int electrodes_count = int.Parse(str_numbers[0]);
            int pairs = int.Parse(str_numbers[1]);
            int meas_per_pair = int.Parse(str_numbers[2]);

            // walidacja wewnątrzplikowa oraz plik-program
            if (pairs != (electrodes_count*(electrodes_count-1))/2)
                throw new FormatException("Niezgodność elektrod i par elektrod");
            if (meas_per_pair != 1)
                throw new FormatException("Macierz pojemności własnych może posiadać tylko jeden pomiar na parę elektrod");
            if (electrodes_count != (int)this.edtNumberOfElectrodes.Value)
                throw new FormatException("Liczba elektrod w pliku pojemności własnych nie zgadza się z ustawieniami w programie");

            float[,] caps = new float[electrodes_count, electrodes_count];
            for (int i = 0; i < pairs; i++)
            {
                string sline = sr.ReadLine();

                // jak mnie to zawsze w... irytuje.
                sline = sline.Replace(".", Application.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                sline = sline.Replace(",", Application.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                str_numbers = sline.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int hi = int.Parse(str_numbers[0]);
                int lo = int.Parse(str_numbers[1]);
                float cap = float.Parse(str_numbers[2]);
                caps[hi-1, lo-1] = cap;
            }
            
            return caps;
        }

        private void btnShowSocketDriverWindow_Click(object sender, EventArgs e)
        {
           // if (socket_driver_window == null)
           // {
            //    this.socket_driver_window = new SocketDriverForm();
                this.socket_driver_window.Show();
          //  }
        }

        private void tbRLCAddress_TextChanged(object sender, EventArgs e)
        {

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
        }
    }
}
