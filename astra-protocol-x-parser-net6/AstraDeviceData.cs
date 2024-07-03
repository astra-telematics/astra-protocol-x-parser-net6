namespace AstraProtocolXParser
{
	public class AstraDeviceData
	{
        public string? model { get; set; }
        public string? imei { get; set; }
        public string? vin { get; set; }
        public string? firmwareVersion { get; set; }
        public string? hardwareRevision { get; set; }
        public string? settingsChecksum { get; set; }

        public AstraDeviceData()
		{
		}
	}
}

