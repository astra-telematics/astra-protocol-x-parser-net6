namespace AstraProtocolXParser.modules
{
	public class AstraProtocolXGpsDataModule
	{
		public static ulong moduleMask = 1 << 1;

        public double latitude { get; set; }
        public double longitude { get; set; }
        public string? gazetteer { get; set; }
        public double speedKmh { get; set; }
        public double maxSpeedSinceLastReportKmh { get; set; }
        public double journeyDistanceKm { get; set; }
        public int headingDeg { get; set; }
        public int? totalOdometerM { get; set; }
        public int altitudeM { get; set; }

        public AstraProtocolXGpsDataModule()
		{
		}
    }
}

