namespace AstraProtocolXParser.modules
{
    public class AstraProtocolXReservedModule
	{
		public static ulong moduleMask = 1UL << 47;

		public AstraProtocolXReservedModuleNVData? nvData { get; set; }

        public AstraProtocolXReservedModule()
		{
		}
	}

	public class AstraProtocolXReservedModuleNVData
	{
		public static byte type = 0x01;

		public ushort flags { get; set; }
		public uint pc { get; set; }
		public uint lr { get; set; }
		public ushort[] watchdogLevels { get; set; } = new ushort[15];
		public string? watchdogServiceName { get; set; }
	}
}