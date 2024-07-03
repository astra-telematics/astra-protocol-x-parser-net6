namespace AstraProtocolXParser.modules
{
    public class AstraProtocolXGsmNetworkInfoModule
    {
        public static ulong moduleMask = 1 << 6;

        public ushort mcc;
        public ushort mnc;

        public AstraProtocolXGsmNetworkInfoModule()
        {
        }
    }
}

