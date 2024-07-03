namespace AstraProtocolXParser.modules
{
	public class AstraProtocolXSignalQualityModule
	{
        public static ulong moduleMask = 1 << 5;

		public ushort gnssSatellitesInUse;
		public int gsmSignalStrength;

        public AstraProtocolXSignalQualityModule()
		{
		}
	}
}

