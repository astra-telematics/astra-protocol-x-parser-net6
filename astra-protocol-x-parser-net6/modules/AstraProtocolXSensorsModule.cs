using AstraProtocolXParser.modules.module_components;

namespace AstraProtocolXParser.modules
{
    public class AstraProtocolXSensorsModule
    {
        public static ulong moduleMask = 0x100000000;

        public List<AstraProtocolXSensor> sensors { get; set; }

        public AstraProtocolXSensorsModule()
        {
            sensors = new List<AstraProtocolXSensor>();
        }
    }
}



