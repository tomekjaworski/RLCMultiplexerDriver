using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net;

namespace MultiplexerLib
{
    public class Multiplexer
    {
        private UInt16[] matrix;
        TcpClient mult;
        bool test;

        public Multiplexer(bool test_mode)
        {
            this.matrix = new UInt16[16];
            this.mult = null;
            this.test = test_mode;
        }


        public void Connect(string address)
        {
            if (test)
                return;

            if (this.mult != null)
                this.mult.Close();

            this.mult = new TcpClient();
            this.mult.SendTimeout = 3000;
            this.mult.ReceiveTimeout = 3000;
            this.mult.Connect(new IPEndPoint(IPAddress.Parse(address), 14000));

        }

        public void SafeDisconnect()
        {
            if (test)
                return;

            try
            {
                this.mult.Close();
                this.mult = null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("SafeDisconnect: " + e.Message);
            }
        }

        public void InternalSetSwitch(int card_id, Switches switch_id, bool state)
        {
            UInt16 sw = (UInt16)switch_id;

            if (state)
                this.matrix[card_id] |= (ushort)(1 << sw); // ustaw bit
            else
                this.matrix[card_id] &= (ushort)(~(1 << sw)); // wyczysc bit

        }

        public void SetSwitch(int card_id, Switches switch_id, bool state)
        {
            this.InternalSetSwitch(card_id, switch_id, state);
            Push();
        }

        public bool GetSwitch(int card_id, Switches switch_id)
        {
            UInt16 sw = (UInt16)switch_id;
            return (this.matrix[card_id] & (ushort)(1 << sw)) > 0;
        }

        /// <summary>
        /// Włącz lub wyłącz diodę LED na wybranej karcie
        /// </summary>
        /// <param name="card">Numer karty, liczony od 0</param>
        /// <param name="state">Stan diody</param>
        public void SetLED(int card, bool state)
        {
            this.SetSwitch(card, Switches.LED, state);
        }

        public bool GetLED(int card)
        {
            return GetSwitch(card, Switches.LED);
        }

        private void InternalSetChannel(int channel_id, ChannelState state)
        {
            channel_id -= 1;
            int card = channel_id % 16;
            int group = channel_id / 16;

            if (group == 0)
            {
                InternalSetSwitch(card, Switches.AH, state == ChannelState.High);
                InternalSetSwitch(card, Switches.AL, state == ChannelState.Low);
                InternalSetSwitch(card, Switches.AG, state == ChannelState.Ground);
            }

            if (group == 1)
            {
                InternalSetSwitch(card, Switches.BH, state == ChannelState.High);
                InternalSetSwitch(card, Switches.BL, state == ChannelState.Low);
                InternalSetSwitch(card, Switches.BG, state == ChannelState.Ground);
            }

            if (group == 2)
            {
                InternalSetSwitch(card, Switches.CH, state == ChannelState.High);
                InternalSetSwitch(card, Switches.CL, state == ChannelState.Low);
                InternalSetSwitch(card, Switches.CG, state == ChannelState.Ground);
            }

            if (group == 3)
            {
                InternalSetSwitch(card, Switches.DH, state == ChannelState.High);
                InternalSetSwitch(card, Switches.DL, state == ChannelState.Low);
                InternalSetSwitch(card, Switches.DG, state == ChannelState.Ground);
            }
        }

        // ********************************************************************************

        /// <summary>
        /// Ustawienie trybu dla wybranego kanału
        /// </summary>
        /// <param name="channel_id">Numer kanału, liczony od 1</param>
        /// <param name="state">Tryb kanału</param>
        public void SetChannel(int channel_id, ChannelState state)
        {
            InternalSetChannel(channel_id, state);
            Push();

        }

        public ChannelState GetChannel(int channel_id)
        {
            channel_id -= 1;
            int card = channel_id % 16;
            int group = channel_id / 16;

            if (group == 0)
            {
                if (GetSwitch(card, Switches.AH) && !GetSwitch(card, Switches.AL) && !GetSwitch(card, Switches.AG)) return ChannelState.High;
                if (!GetSwitch(card, Switches.AH) && GetSwitch(card, Switches.AL) && !GetSwitch(card, Switches.AG)) return ChannelState.Low;
                if (!GetSwitch(card, Switches.AH) && !GetSwitch(card, Switches.AL) && GetSwitch(card, Switches.AG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            if (group == 1)
            {
                if (GetSwitch(card, Switches.BH) && !GetSwitch(card, Switches.BL) && !GetSwitch(card, Switches.BG)) return ChannelState.High;
                if (!GetSwitch(card, Switches.BH) && GetSwitch(card, Switches.BL) && !GetSwitch(card, Switches.BG)) return ChannelState.Low;
                if (!GetSwitch(card, Switches.BH) && !GetSwitch(card, Switches.BL) && GetSwitch(card, Switches.BG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            if (group == 2)
            {
                if (GetSwitch(card, Switches.CH) && !GetSwitch(card, Switches.CL) && !GetSwitch(card, Switches.CG)) return ChannelState.High;
                if (!GetSwitch(card, Switches.CH) && GetSwitch(card, Switches.CL) && !GetSwitch(card, Switches.CG)) return ChannelState.Low;
                if (!GetSwitch(card, Switches.CH) && !GetSwitch(card, Switches.CL) && GetSwitch(card, Switches.CG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            if (group == 3)
            {
                if (GetSwitch(card, Switches.DH) && !GetSwitch(card, Switches.DL) && !GetSwitch(card, Switches.DG)) return ChannelState.High;
                if (!GetSwitch(card, Switches.DH) && GetSwitch(card, Switches.DL) && !GetSwitch(card, Switches.DG)) return ChannelState.Low;
                if (!GetSwitch(card, Switches.DH) && !GetSwitch(card, Switches.DL) && GetSwitch(card, Switches.DG)) return ChannelState.Ground;
                return ChannelState.None;
            }

            return ChannelState.None;
        }

        public void SetAllChannels(ChannelState channelState)
        {
            for (int i = 0; i < 64; i++)
                this.InternalSetChannel(i + 1, channelState);

            this.Push();
        }

        // ********************************************************************************


        private void Push()
        {
            if (test)
                return;

            if (this.mult == null || !this.mult.Connected)
                throw new Exception("Brak połączenia z multiplekserem.");

            NetworkStream ns = this.mult.GetStream();

            byte[] buff = new byte[this.matrix.Length * 2];
            for (int i = 0; i < this.matrix.Length; i++)
            {
                short data = IPAddress.NetworkToHostOrder((short)this.matrix[i]);
                buff[i * 2 + 1] = (byte)(data >> 8);
                buff[i * 2 + 0] = (byte)(data);
            }

            ns.Write(buff, 0, buff.Length);
        }



        public void SetAllLED(bool state)
        {
            for (int i = 0; i < 16; i++)
                this.InternalSetSwitch(i, Switches.LED,state);

            this.Push();
        }
    }

    

    
    public enum Switches
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
}
