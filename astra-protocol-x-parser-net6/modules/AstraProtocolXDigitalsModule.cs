using System;
using System.Text.Json.Serialization;

namespace AstraProtocolXParser.modules
{
    public class AstraProtocolXDigitalsModule
    {
        public static ulong moduleMask = 1 << 2;

        public uint currentStatesMask;
        public uint changesMask;

        public uint digin1State { get; set; }
        public uint digin2State { get; set; }
        public uint digin3State { get; set; }
        public uint digin4State { get; set; }
        public uint digin5State { get; set; }
        public uint digin6State { get; set; }
        public uint digin7State { get; set; }
        public uint digin8State { get; set; }

        public uint digout1State { get; set; }
        public uint digout2State { get; set; }
        public uint digout3State { get; set; }
        public uint digout4State { get; set; }
        public uint digout5State { get; set; }
        public uint digout6State { get; set; }
        public uint digout7State { get; set; }
        public uint digout8State { get; set; }

        public AstraProtocolXDigitalsModule()
        {
        }

        private uint inState (int inNumber)
        {
            int bitPos = inNumber - 1;
            int mask = 1 << bitPos;
            if ((this.currentStatesMask & mask) == mask)
            {
                return 1;
            }

             return 0;
        }

        private uint outState(int inNumber)
        {
            int bitPos = inNumber + 7;
            int mask = 1 << bitPos;
            if ((this.currentStatesMask & mask) == mask)
            {
                return 1;
            }

            return 0;
        }

        private uint inChanged(int inNumber)
        {
            int bitPos = inNumber - 1;
            int mask = 1 << bitPos;
            if ((this.changesMask & mask) == mask)
            {
                return 1;
            }

            return 0;
        }

        private uint outChanged(int inNumber)
        {
            int bitPos = inNumber + 7;
            int mask = 1 << bitPos;
            if ((this.changesMask & mask) == mask)
            {
                return 1;
            }

            return 0;
        }

        public void setStates()
        {
            this.digin1State = inState(1);
            this.digin2State = inState(2);
            this.digin3State = inState(3);
            this.digin4State = inState(4);
            this.digin5State = inState(5);
            this.digin6State = inState(6);
            this.digin7State = inState(7);
            this.digin8State = inState(8);

            this.digout1State = outState(1);
            this.digout2State = outState(2);
            this.digout3State = outState(3);
            this.digout4State = outState(4);
            this.digout5State = outState(5);
            this.digout6State = outState(6);
            this.digout7State = outState(7);
            this.digout8State = outState(8);
        }
    }
}


