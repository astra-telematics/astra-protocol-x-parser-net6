namespace AstraProtocolXParser.modules
{
    public class AstraProtocolXBatteryUsageStatisticsModule
    {
        public static ulong moduleMask = 1UL << 40;

        public uint gnssAcquiringTimeS { get; set; }
        public uint gnssFixingTimeS { get; set; }
        public uint modemOnTimeS { get; set; }
        public uint modemCsRegisteredTimeS { get; set; }
        public uint modemPsRegisteredTimeS { get; set; }
        public uint pdpActiveTimeS { get; set; }
        public uint wakeTimeS { get; set; }
        public uint sleepTimeS { get; set; }
        public uint bleOnTimeS { get; set; }
        public uint wakeCycleCount { get; set; }
        public uint reportsSent { get; set; }
        public uint externalPowerOnTimeS { get; set; }
        public uint socketOpenTimeS { get; set; }

        public AstraProtocolXBatteryUsageStatisticsModule()
        {
        }
    }
}

