using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace MultiplexerLib
{
    public class Multiplexer
    {
        private UInt16[] matrix;
        TcpClient client;
        NetworkStream stream;


        //bool IsConnected { get { if (this.mult == null) return false; return true; } }
        public  bool Connected { get {
                if (this.client == null)
                    return false;
                return this.client.Connected; } }

        public bool TestMode { get; set; }


        public Multiplexer(bool test_mode)
        {
            this.matrix = new UInt16[16];
            this.client = null;
            this.stream = null;
            this.TestMode = test_mode;
        }


        public async void Connect(string address)
        {
            if (this.TestMode)
                return;

            if (this.client != null)
            {
                this.stream.Close();
                this.client.Close();
            }

            this.client = new TcpClient();
            this.client.SendTimeout = 3000;
            this.client.ReceiveTimeout = 3000;

            await this.client.ConnectAsync(IPAddress.Parse(address), 14000);
            this.stream = this.client.GetStream();
        }

        public void SafeDisconnect()
        {
            if (this.TestMode)
                return;

            try
            {
                this.client.Close();
                this.client = null;
                this.stream = null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("SafeDisconnect: " + e.Message);
            }
        }


        private void InternalSetSwitch(int card_id, Switch switch_id, bool state)
        {
            UInt16 sw = (UInt16)switch_id;

            if (state)
                this.matrix[card_id] |= (ushort)(1 << sw); // ustaw bit
            else
                this.matrix[card_id] &= (ushort)(~(1 << sw)); // wyczysc bit

        }

        public Task<bool> SetSwitch(int card_id, Switch switch_id, bool state)
        {
            this.InternalSetSwitch(card_id, switch_id, state);
            return this.Push();
        }

        public bool GetSwitch(int card_id, Switch switch_id)
        {
            UInt16 sw = (UInt16)switch_id;
            return (this.matrix[card_id] & (ushort)(1 << sw)) > 0;
        }

        /// <summary>
        /// Włącz lub wyłącz diodę LED na wybranej karcie
        /// </summary>
        /// <param name="card">Numer karty, liczony od 0</param>
        /// <param name="state">Stan diody</param>
        public Task SetLED(int card, bool state)
        {
            return this.SetSwitch(card, Switch.LED, state);
        }

        public bool GetLED(int card)
        {
            return GetSwitch(card, Switch.LED);
        }

        private void InternalSetChannel(int channel_id, ChannelState state)
        {
            channel_id -= 1;
            int card_id = channel_id % 16;
            int socket_id = channel_id / 16;

            if (socket_id == 0)
            {
                InternalSetSwitch(card_id, Switch.AH, state == ChannelState.High);
                InternalSetSwitch(card_id, Switch.AL, state == ChannelState.Low);
                InternalSetSwitch(card_id, Switch.AG, state == ChannelState.Ground);
            }

            if (socket_id == 1)
            {
                InternalSetSwitch(card_id, Switch.BH, state == ChannelState.High);
                InternalSetSwitch(card_id, Switch.BL, state == ChannelState.Low);
                InternalSetSwitch(card_id, Switch.BG, state == ChannelState.Ground);
            }

            if (socket_id == 2)
            {
                InternalSetSwitch(card_id, Switch.CH, state == ChannelState.High);
                InternalSetSwitch(card_id, Switch.CL, state == ChannelState.Low);
                InternalSetSwitch(card_id, Switch.CG, state == ChannelState.Ground);
            }

            if (socket_id == 3)
            {
                InternalSetSwitch(card_id, Switch.DH, state == ChannelState.High);
                InternalSetSwitch(card_id, Switch.DL, state == ChannelState.Low);
                InternalSetSwitch(card_id, Switch.DG, state == ChannelState.Ground);
            }
        }

        // ********************************************************************************

        /// <summary>
        /// Ustawienie trybu dla wybranego kanału
        /// </summary>
        /// <param name="channel_id">Numer kanału, liczony od 1</param>
        /// <param name="state">Tryb kanału</param>
        public Task<bool> SetChannel(int channel_id, ChannelState state)
        {
            InternalSetChannel(channel_id, state);
            return this.Push();

        }

        public Task<bool> SetChannel(int c1, ChannelState s1, int c2, ChannelState s2)
        {
            InternalSetChannel(c1, s1);
            InternalSetChannel(c2, s2);
            return this.Push();

        }

        public ChannelState GetChannel(int channel_id)
        {
            channel_id -= 1;
            int card = channel_id % 16;
            int group = channel_id / 16;

            if (group == 0)
            {
                if (GetSwitch(card, Switch.AH) && !GetSwitch(card, Switch.AL) && !GetSwitch(card, Switch.AG)) return ChannelState.High;
                if (!GetSwitch(card, Switch.AH) && GetSwitch(card, Switch.AL) && !GetSwitch(card, Switch.AG)) return ChannelState.Low;
                if (!GetSwitch(card, Switch.AH) && !GetSwitch(card, Switch.AL) && GetSwitch(card, Switch.AG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            if (group == 1)
            {
                if (GetSwitch(card, Switch.BH) && !GetSwitch(card, Switch.BL) && !GetSwitch(card, Switch.BG)) return ChannelState.High;
                if (!GetSwitch(card, Switch.BH) && GetSwitch(card, Switch.BL) && !GetSwitch(card, Switch.BG)) return ChannelState.Low;
                if (!GetSwitch(card, Switch.BH) && !GetSwitch(card, Switch.BL) && GetSwitch(card, Switch.BG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            if (group == 2)
            {
                if (GetSwitch(card, Switch.CH) && !GetSwitch(card, Switch.CL) && !GetSwitch(card, Switch.CG)) return ChannelState.High;
                if (!GetSwitch(card, Switch.CH) && GetSwitch(card, Switch.CL) && !GetSwitch(card, Switch.CG)) return ChannelState.Low;
                if (!GetSwitch(card, Switch.CH) && !GetSwitch(card, Switch.CL) && GetSwitch(card, Switch.CG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            if (group == 3)
            {
                if (GetSwitch(card, Switch.DH) && !GetSwitch(card, Switch.DL) && !GetSwitch(card, Switch.DG)) return ChannelState.High;
                if (!GetSwitch(card, Switch.DH) && GetSwitch(card, Switch.DL) && !GetSwitch(card, Switch.DG)) return ChannelState.Low;
                if (!GetSwitch(card, Switch.DH) && !GetSwitch(card, Switch.DL) && GetSwitch(card, Switch.DG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            return ChannelState.None;
        }

        public Task<bool> SetAllChannels(ChannelState channelState)
        {
            for (int i = 0; i < 64; i++)
                this.InternalSetChannel(i + 1, channelState);

            return this.Push();
        }

        // ********************************************************************************


        private Task<bool> Push()
        {
            if (this.TestMode)
                return Task.FromResult<bool>(true);

            if (!this.client.Connected)
                throw new Exception("Brak połączenia z multiplekserem.");

            //NetworkStream ns = this.mult.GetStream();

            byte[] buff = new byte[2 + this.matrix.Length * 2];

            // polecenie
            Command cmd = Command.Push;
            short data = IPAddress.NetworkToHostOrder((short)cmd);
            buff[1] = (byte)(data >> 8);
            buff[0] = (byte)(data);

            for (int i = 0; i < this.matrix.Length; i++)
            {
                data = IPAddress.NetworkToHostOrder((short)this.matrix[i]);
                buff[i * 2 + 1 + 2] = (byte)(data >> 8);
                buff[i * 2 + 0 + 2] = (byte)(data);
            }

            return this.TrySendBuffer(buff);
        }

        private async Task<bool> TrySendBuffer(byte[] buff)
        {
            try
            {
                await this.stream.WriteAsync(buff, 0, buff.Length);
                return true;
            } catch(Exception e)
            {
                //
                return false;
            }
        }

        public Task<bool> Ping()
        {
            if (this.TestMode)
                return Task.FromResult<bool>(true);

            if (!this.client.Connected)
                throw new Exception("Brak połączenia z multiplekserem.");

            byte[] buff = new byte[2 + this.matrix.Length * 2];

            // polecenie
            Command cmd = Command.Ping;
            short data = IPAddress.NetworkToHostOrder((short)cmd);
            buff[1] = (byte)(data >> 8);
            buff[0] = (byte)(data);

            for (int i = 0; i < this.matrix.Length; i++)
            {
                data = IPAddress.NetworkToHostOrder((short)this.matrix[i]);
                buff[i * 2 + 1 + 2] = (byte)(data >> 8);
                buff[i * 2 + 0 + 2] = (byte)(data);
            }


            return this.TrySendBuffer(buff);
        }





        public Task<bool> SetAllLED(bool state)
        {
            for (int i = 0; i < 16; i++)
                this.InternalSetSwitch(i, Switch.LED,state);

            return this.Push();
        }
    }

    

    
    public enum Switch
    {
        Reserved0 = 0,
        Reserved1 = 1,
        Reserved2 = 2,
        LED = 3,
        AG = 4,
        AL = 5,
        AH = 6,
        BG = 7,
        BL = 8,
        BH = 9,
        CG = 10,
        CL = 11,
        CH = 12,
        DG = 13,
        DL = 14,
        DH = 15,
    }

    public enum ChannelState
    {
        High,
        Low,
        Ground,
        None
    }

    public enum Command : ushort
    {
        Push = 0x0001,
        Ping = 0x0002,
    }
}
