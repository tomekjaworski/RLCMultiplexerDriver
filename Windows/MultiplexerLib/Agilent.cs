using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace MultiplexerLib
{
    public class Agilent
    {
        TcpClient cli;
        NetworkStream ns;
        StreamReader sr;
        List<string> log;

        public Agilent()
        {
            this.cli = null;
            this.ns = null;
            this.sr = null;
            this.log = new List<string>();
        }

        public void Connect(string host, int read_timeout)
        {
            InternalClose();

            this.cli = new TcpClient();
            this.cli.ReceiveTimeout = read_timeout;
            this.cli.SendTimeout = 2000;
            this.cli.Connect(host, 5025);

            this.ns = this.cli.GetStream();
            this.sr = new StreamReader(this.ns);


            string response;
            
            InternalSendCommand("*opt?");
            response = InternalGetResponse();

            if (String.IsNullOrEmpty(response))
            {
                this.InternalClose();
                throw new Exception("Brak komunikacji z mostkiem");
            }


            response = this.Chatter(":abort", false);
            response = this.Chatter("*cls", false);
            response = this.Chatter(":system:error:next?", true);
            response = this.Chatter(":system:error:next?", true);
            response = this.Chatter(":system:error:next?", true);
        }

        public void SendConfiguration(bool parallel, int frequency, double voltage, double trigger_delay, double step_delay, int average)
        {
            this.RestartMeasurementCycle();

            // format danych zwracanych przez mostek
            this.Chatter(":format:data ascii", false);
            this.Chatter(":format:data?", true);

            // długi format
            this.Chatter(":format:ascii:long ON", false);
            this.Chatter(":format:ascii:long?", true);

            // wyzwalanie pomiaru
            this.Chatter(":trigger:source bus", false);
            this.Chatter(":trigger:source?", true);


            // tryb pomiaru (rownolegly/szeregowy)
            if (parallel)
                this.Chatter(":function:impedance CpD", false);
            else
                this.Chatter(":function:impedance CsD", false);
            this.Chatter(":function:impedance?", true);

            this.Chatter(":frequency " + frequency.ToString(), false);
            this.Chatter(":frequency?", true);
            this.Chatter(":voltage " + voltage.ToString("N3").Replace(',', '.'), false);
            this.Chatter(":voltage?", true);

            this.Chatter(":aperture medium," + average.ToString(), false);
            this.Chatter(":aperture?", true);


            this.Chatter(":trigger:delay " + step_delay.ToString("N3").Replace(',', '.'), false);
            this.Chatter(":trigger:delay?", true);
            this.Chatter(":trigger:tdel " + trigger_delay.ToString("N3").Replace(',','.'), false);
            this.Chatter(":trigger:tdel?", true);


            this.ShowMessage("Gotowy.");

            ////

            //string s = this.Chatter(":trigger:immediate", false);
            // s = this.Chatter(":fetch:impedance?", true);

        }

        public string Chatter(string command, bool receive_response)
        {
            this.InternalSendCommand(command);
            if (!receive_response)
            {
                this.log.Add(command);
                return null;
            }

            string resp = this.InternalGetResponse();
            if (resp == null) resp = "<null>";
            if (resp == "") resp = "<empty>";

            this.log.Add(string.Format("{0} => {1}", command, resp));
            return resp;
        }

        private void InternalClose()
        {
            try
            {
                if (this.cli != null)
                {
                    this.sr.Close();
                    this.ns.Close();
                    this.cli.Close();
                }

                this.cli = null;
                this.ns = null;
                this.sr = null;
            }
            catch (Exception e)
            {
            }
        }

        private string InternalGetResponse()
        {
            try
            {
                string resp = this.sr.ReadLine();
                if (resp == null)
                {
                    this.InternalClose();
                    throw new SocketException((int)SocketError.ConnectionAborted);
                }
                return resp;
            }
            catch (IOException e)
            {
                if (!(e.InnerException is SocketException))
                    throw e;

                SocketException se = e.InnerException as SocketException;
                if (se.SocketErrorCode == SocketError.TimedOut)
                    return null;

                // to nie jest timeout... wywal wyjatek
                throw e;
            }


        }

        private void InternalSendCommand(string command)
        {
            command += "\n";

            byte[] buff = Encoding.ASCII.GetBytes(command);
            this.ns.Write(buff, 0, buff.Length);


        }

        public void SafeDisconnect()
        {
            this.InternalClose();
        }

        public double MeasureFrequency()
        {
            string s = this.Chatter(":trigger:immediate", false);
            s = this.Chatter(":fetch:impedance?", true);

            s = s.Replace(',', ';');
            s = s.Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            s = s.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            string[] data = s.Split(';');
            double capacity = double.Parse(data[0]);
            double d = double.Parse(data[1]);
            double unknown = double.Parse(data[2]);

            //if (capacity > 10)
            //    capacity = -1; // overload

            return capacity;

        }


        public void ShowMessage(string message)
        {
            message = '"' + message + '"';
            this.Chatter(":display:line " + message, false);
        }

        public void RestartMeasurementCycle()
        {
            // ustawienie trybu pomiaru
            this.Chatter(":abort", false);
            this.Chatter(":display:cclear", false);
            this.Chatter(":display:page measurement", false);
            this.Chatter(":display:page?", true);
        }
    }
}

