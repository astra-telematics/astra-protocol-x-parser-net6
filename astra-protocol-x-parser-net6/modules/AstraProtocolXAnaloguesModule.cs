namespace AstraProtocolXParser.modules
{
    public class AstraProtocolXAnaloguesModule
    {
        public static ulong moduleMask = 1 << 3;

        public ushort adc1Value;
        public ushort adc2Value;

        public AstraProtocolXAnaloguesModule()
        {
        }
    }
}

