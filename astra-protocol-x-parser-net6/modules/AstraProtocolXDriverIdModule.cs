using System;
using System.Text.Json.Serialization;
using AstraProtocolXParser.enums;

namespace AstraProtocolXParser.modules
{
	public class AstraProtocolXDriverIdModule
	{
        public static ulong moduleMask = 1 << 8;

		public AstraProtocolXDriverIdSource source { get; set; }
        public List<byte>? serialNumber { get; set; }

        public AstraProtocolXDriverIdModule()
		{
		}
	}
}

