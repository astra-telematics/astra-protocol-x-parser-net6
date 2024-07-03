namespace AstraProtocolXParser.modules
{
	public class AstraProtocolXDevicePowerModule
	{
		public static ulong moduleMask = 1 << 0;

        public double externalVoltageV { get; set; }
        public uint batteryLevelPercent { get; set; }

        public AstraProtocolXDevicePowerModule()
		{
		}
    }
}

