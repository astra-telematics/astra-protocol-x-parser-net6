using System;
using AstraProtocolXParser.enums;

namespace AstraProtocolXParser.modules.module_components
{
	public class AstraProtocolXSensor
	{
		public AstraProtocolXSensorType type;

		public double? tempC { get; set; }
		public double? humidityPercent { get; set; }
		public bool? dataValid { get; set; }

		public AstraProtocolXSensor()
		{
		}
	}
}

