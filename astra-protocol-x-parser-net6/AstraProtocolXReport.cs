using AstraProtocolXParser.enums;
using AstraProtocolXParser.modules;
using AstraProtocolXParser.modules.module_components;

namespace AstraProtocolXParser
{
    public class AstraProtocolXReport
    {
        public int sequenceNumber { get; set; }
        public ulong moduleMask { get; set; }
        public DateTime reportTimeUtc { get; set; }
        public int? ignitionState { get; set; }
        public int? gpsValid { get; set; }
        public List<AstraProtocolXReportReason> reasons { get; set; } = new List<AstraProtocolXReportReason>();
        public List<String>? reasonMnemonics { get; set; }
        public AstraProtocolXDevicePowerModule? devicePower { get; set; }
        public AstraProtocolXGpsDataModule? gpsData { get; set; }
        public AstraProtocolXDigitalsModule? digitals { get; set; }
        public AstraProtocolXAnaloguesModule? analogues { get; set; }
        public AstraProtocolXDriverBehaviourModule? driverBehaviour { get; set; }
        public AstraProtocolXSignalQualityModule? signalQuality { get; set; }
        public AstraProtocolXGeofencesModule? geofences { get; set; }
        public AstraProtocolXDriverIdModule? driverId { get; set; }
        public AstraProtocolXGsmNetworkInfoModule? gsmNetworkInfo { get; set; }
        public AstraProtocolXSensorsModule? sensorData { get; set; }
        public AstraProtocolXReservedModule? reservedData { get; set; }
        public AstraProtocolXBatteryUsageStatisticsModule? batteryUsageStatistics { get; set; }

        public AstraProtocolXReport()
		{
		}

		public static AstraProtocolXReport? fromBytes(byte[] bytes, int bytesLength, ref int byteIndex, ref string error)
		{
			AstraProtocolXReport report = new();

            int initialByteIndex = byteIndex;

            // sequence number
            if (byteIndex >= bytesLength - 1)
			{
				error = "not enough bytes to read sequence number";
				return null;
			}
			report.sequenceNumber = bytes[byteIndex++];

            // module mask (48-bit)
            if (byteIndex >= bytesLength - 6)
            {
                error = "not enough bytes to read module mask";
                return null;
            }
            report.moduleMask = bytes[byteIndex++];
			report.moduleMask <<= 8;
            report.moduleMask |= bytes[byteIndex++];
            report.moduleMask <<= 8;
            report.moduleMask |= bytes[byteIndex++];
            report.moduleMask <<= 8;
            report.moduleMask |= bytes[byteIndex++];
            report.moduleMask <<= 8;
            report.moduleMask |= bytes[byteIndex++];
            report.moduleMask <<= 8;
            report.moduleMask |= bytes[byteIndex++];

            // report time
            if (byteIndex >= bytesLength - 4)
            {
                error = "not enough bytes to read report time";
                return null;
            }
            uint julianSecs = Utils.parseU32(ref bytes, ref byteIndex);

            report.reportTimeUtc = DateTime.Parse("1980-01-06T00:00:00+0000").AddSeconds(julianSecs);
            report.reportTimeUtc = DateTime.SpecifyKind(report.reportTimeUtc, DateTimeKind.Utc);

            // reason flags
            if (byteIndex >= bytesLength - 4)
            {
                error = "not enough bytes to read reason flags";
                return null;
            }

            uint reasonFlags = Utils.parseU32(ref bytes, ref byteIndex);

            // convert to interperatable reasons
            for (int i = 0; i < (uint)AstraProtocolXReportReason.COUNT; i++)
            {
                uint mask = (uint)1 << i;
                if ((reasonFlags & mask) == mask)
                {
                    report.reasons.Add((AstraProtocolXReportReason)i);
                }
            }

            report.reasonMnemonics = new List<string>();

            for (int i = 0; i < report.reasons.Count; i++)
            {
                switch (report.reasons[i])
                {
                    case AstraProtocolXReportReason.TIMED_INTERVAL_ELAPSED:
                        report.reasonMnemonics.Add("TIMED_INTERVAL");
                        break;
                    case AstraProtocolXReportReason.DISTANCE_TRAVELLED_EXCEEDED:
                        report.reasonMnemonics.Add("DISTANCE_TRAVELLED_EXCEEDED");
                        break;
                    case AstraProtocolXReportReason.POSITION_ON_DEMAND:
                        report.reasonMnemonics.Add("POSITION_ON_DEMAND");
                        break;
                    case AstraProtocolXReportReason.GEOFENCE:
                        report.reasonMnemonics.Add("GEOFENCE");
                        break;
                    case AstraProtocolXReportReason.PANIC_SWITCH_ACTIVATED:
                        report.reasonMnemonics.Add("PANIC");
                        break;
                    case AstraProtocolXReportReason.EXTERNAL_IO:
                        report.reasonMnemonics.Add("EXTERNAL_IO");
                        break;
                    case AstraProtocolXReportReason.JOURNEY_START:
                        report.reasonMnemonics.Add("JOURNEY_START");
                        break;
                    case AstraProtocolXReportReason.JOURNEY_STOP:
                        report.reasonMnemonics.Add("JOURNEY_STOP");
                        break;
                    case AstraProtocolXReportReason.HEADING_CHANGE:
                        report.reasonMnemonics.Add("HEADING_CHANGE");
                        break;
                    case AstraProtocolXReportReason.LOW_BATTERY:
                        report.reasonMnemonics.Add("LOW_BATTERY");
                        break;
                    case AstraProtocolXReportReason.EXTERNAL_POWER:
                        report.reasonMnemonics.Add("EXTERNAL_POWER");
                        break;
                    case AstraProtocolXReportReason.IDLING_START:
                        report.reasonMnemonics.Add("IDLING_START");
                        break;
                    case AstraProtocolXReportReason.IDLING_END:
                        report.reasonMnemonics.Add("IDLING_END");
                        break;
                    case AstraProtocolXReportReason.IDLING_ONGOING:
                        report.reasonMnemonics.Add("IDLING_ONGOING");
                        break;
                    case AstraProtocolXReportReason.REBOOT:
                        report.reasonMnemonics.Add("REBOOT");
                        break;
                    case AstraProtocolXReportReason.SPEED_OVERTHRESHOLD:
                        report.reasonMnemonics.Add("SPEED_OVER_THRESHOLD");
                        break;
                    case AstraProtocolXReportReason.TOWING:
                        report.reasonMnemonics.Add("TOWING");
                        break;
                    case AstraProtocolXReportReason.UNAUTHORISED_DRIVER_ALARM:
                        report.reasonMnemonics.Add("UNAUTHORISED_DRIVER_ALARM");
                        break;
                    case AstraProtocolXReportReason.COLLISION_ALARM:
                        report.reasonMnemonics.Add("COLLISION_ALARM");
                        break;
                    case AstraProtocolXReportReason.ACCEL_THRESHOLD_MAX:
                        report.reasonMnemonics.Add("ACCEL_THRESHOLD_MAX");
                        break;
                    case AstraProtocolXReportReason.CORNERING_THRESHOLD_MAX:
                        report.reasonMnemonics.Add("ACCEL_THRESHOLD_MAX");
                        break;
                    case AstraProtocolXReportReason.DECEL_THRESHOLD_MAX:
                        report.reasonMnemonics.Add("DECEL_THRESHOLD_MAX");
                        break;
                    case AstraProtocolXReportReason.GPS_REACQUIRED:
                        report.reasonMnemonics.Add("GPS_REACQUIRED");
                        break;
                    case AstraProtocolXReportReason.CANBUS:
                        report.reasonMnemonics.Add("CANBUS");
                        break;
                    case AstraProtocolXReportReason.CARRIER:
                        report.reasonMnemonics.Add("CARRIER");
                        break;
                    case AstraProtocolXReportReason.TAMPER:
                        report.reasonMnemonics.Add("TAMPER_ALARM");
                        break;
                    case AstraProtocolXReportReason.TOWING_END:
                        report.reasonMnemonics.Add("TOWING_ENDED");
                        break;
                    case AstraProtocolXReportReason.SERIAL_DEVICE:
                        report.reasonMnemonics.Add("SERIAL_CHANGE_OF_STATUS");
                        break;
                    case AstraProtocolXReportReason.LOW_EXTERNAL_VOLTAGE:
                        report.reasonMnemonics.Add("LOW_EXTERNAL_VOLTAGE");
                        break;
                    case AstraProtocolXReportReason.ENTERING_SLEEP_MODE:
                        report.reasonMnemonics.Add("ENTERING_SLEEP_MODE");
                        break;
                    case AstraProtocolXReportReason.ROLL_OVER:
                        report.reasonMnemonics.Add("ROLL_OVER");
                        break;
                    case AstraProtocolXReportReason.MOTION_DETECTED_WHILST_PARKED:
                        report.reasonMnemonics.Add("MOTION_WHILST_PARKED");
                        break;
                }
            }


            // status flags
            if (byteIndex >= bytesLength - 4)
            {
                error = "not enough bytes to read status flags";
                return null;
            }
            ushort statusFlags = Utils.parseU16(ref bytes, ref byteIndex);

            report.gpsValid = (statusFlags & 0x04) == 0x04 ? 0 : 1;
            report.ignitionState = (statusFlags & 0x01) == 0x01 ? 1 : 0;

            // is device power present?
            if ((report.moduleMask & AstraProtocolXDevicePowerModule.moduleMask) == AstraProtocolXDevicePowerModule.moduleMask)
			{
				report.devicePower = new();
				report.devicePower.externalVoltageV = bytes[byteIndex++] * 0.2;
				report.devicePower.batteryLevelPercent = bytes[byteIndex++];
			}

            // is gps data present?
            if ((report.moduleMask & AstraProtocolXGpsDataModule.moduleMask) == AstraProtocolXGpsDataModule.moduleMask)
            {
                report.gpsData = new();
                // skip time
                byteIndex += 4;

                // latitude
                int latInt = Utils.parseS32(ref bytes, ref byteIndex);
                report.gpsData.latitude = (double)latInt / 1000000.0;
                // longitude
                int lonInt = Utils.parseS32(ref bytes, ref byteIndex);
                report.gpsData.longitude = (double)lonInt / 1000000.0;

                // speed
                report.gpsData.speedKmh = bytes[byteIndex++] * 2;

                // max speed since last report
                report.gpsData.maxSpeedSinceLastReportKmh = bytes[byteIndex++] * 2;

                // heading
                report.gpsData.headingDeg = bytes[byteIndex++] * 2;

                // altitude
                report.gpsData.altitudeM = bytes[byteIndex++] * 20;

                // journey distance
                report.gpsData.journeyDistanceKm = Utils.parseU16(ref bytes, ref byteIndex);
                report.gpsData.journeyDistanceKm /= 10;
            }

            // are digitals present?
            if ((report.moduleMask & AstraProtocolXDigitalsModule.moduleMask) == AstraProtocolXDigitalsModule.moduleMask)
            {
                report.digitals = new();
                report.digitals.currentStatesMask = Utils.parseU16(ref bytes, ref byteIndex);
                report.digitals.changesMask = Utils.parseU16(ref bytes, ref byteIndex);
                report.digitals.setStates();
            }

            // are analogues present?
            if ((report.moduleMask & AstraProtocolXAnaloguesModule.moduleMask) == AstraProtocolXAnaloguesModule.moduleMask)
            {
                report.analogues = new();
                report.analogues.adc1Value = Utils.parseU16(ref bytes, ref byteIndex);
                report.analogues.adc2Value = Utils.parseU16(ref bytes, ref byteIndex);
            }

            // is driver behaviour present?
            if ((report.moduleMask & AstraProtocolXDriverBehaviourModule.moduleMask) == AstraProtocolXDriverBehaviourModule.moduleMask)
            {
                report.driverBehaviour = new();
                report.driverBehaviour.accelXMax = (float)bytes[byteIndex++] / 10;
                report.driverBehaviour.accelXMin = (float)bytes[byteIndex++] / 10;
                report.driverBehaviour.accelYMax = (float)bytes[byteIndex++] / 10;
                report.driverBehaviour.accelYMin = (float)bytes[byteIndex++] / 10;
                report.driverBehaviour.accelZMax = (float)bytes[byteIndex++] / 10;
                report.driverBehaviour.accelZMin = (float)bytes[byteIndex++] / 10;
                report.driverBehaviour.idleTimeS = Utils.parseU16(ref bytes, ref byteIndex);
            }

            // is signal quality present?
            if ((report.moduleMask & AstraProtocolXSignalQualityModule.moduleMask) == AstraProtocolXSignalQualityModule.moduleMask)
            {
                byte signalQualityByte = bytes[byteIndex++];
                report.signalQuality = new();
                report.signalQuality.gnssSatellitesInUse = (ushort)(signalQualityByte & 0x0F);
                report.signalQuality.gsmSignalStrength = -111 + (((signalQualityByte >> 4) & 0x0F) * 4);
            }

            // is gsm network info present?
            if ((report.moduleMask & AstraProtocolXGsmNetworkInfoModule.moduleMask) == AstraProtocolXGsmNetworkInfoModule.moduleMask)
            {
                report.gsmNetworkInfo = new();
                report.gsmNetworkInfo.mcc = Utils.parseU16(ref bytes, ref byteIndex);
                report.gsmNetworkInfo.mnc = Utils.parseU16(ref bytes, ref byteIndex);
            }

            // are geofences present?
            if ((report.moduleMask & AstraProtocolXGeofencesModule.moduleMask) == AstraProtocolXGeofencesModule.moduleMask)
            {
                report.geofences = new();
                report.geofences.eventIndex = bytes[byteIndex++];
            }

            // is driver id present?
            if ((report.moduleMask & AstraProtocolXDriverIdModule.moduleMask) == AstraProtocolXDriverIdModule.moduleMask)
            {
                report.driverId = new();
                report.driverId.source = AstraProtocolXDriverIdSource.NONE;
                switch (bytes[byteIndex++])
                {
                    case 1:
                        report.driverId.source = AstraProtocolXDriverIdSource.IBUTTON;
                        break;
                    case 2:
                        report.driverId.source = AstraProtocolXDriverIdSource.RFID;
                        break;
                    case 3:
                        report.driverId.source = AstraProtocolXDriverIdSource.BLUETOOTH;
                        break;
                    case 4:
                        report.driverId.source = AstraProtocolXDriverIdSource.CR002_CARD_READER;
                        break;
                }

                report.driverId.serialNumber = new List<byte>();
                for (int i = 0; i < 8; i++)
                {
                    report.driverId.serialNumber.Add(bytes[byteIndex++]);
                }
            }

            // is trailer id present?
            if ((report.moduleMask & AstraProtocolXTrailerIdModule.moduleMask) == AstraProtocolXTrailerIdModule.moduleMask)
            {
                // skip trailer id
                byteIndex += 12;
            }

            // is fms journey stop data present?
            if ((report.moduleMask & AstraProtocolXFmsJourneyStartDataModule.moduleMask) == AstraProtocolXFmsJourneyStartDataModule.moduleMask)
            {
                // skip trailer id
                byteIndex += 12;
            }

            // is gnss stop report data present?
            if ((report.moduleMask & AstraProtocolXGnssStopReportDataModule.moduleMask) == AstraProtocolXGnssStopReportDataModule.moduleMask)
            {
                // skip gnss stop report data
                byteIndex += 5;
            }

            // is fms in journey data present?
            if ((report.moduleMask & AstraProtocolXFmsInJourneyDataModule.moduleMask) == AstraProtocolXFmsInJourneyDataModule.moduleMask)
            {
                // skip fms in journey data
                byteIndex += 20;
            }

            // is obd in journey data present?
            if ((report.moduleMask & AstraProtocolXObdInJourneyDataModule.moduleMask) == AstraProtocolXObdInJourneyDataModule.moduleMask)
            {
                // skip obd in journey data
                byteIndex += 18;
            }

            // are obd diagnostic trouble codes present?
            if ((report.moduleMask & AstraProtocolXObdDiagnosticTroubleCodesModule.moduleMask) == AstraProtocolXObdInJourneyDataModule.moduleMask)
            {
                // skip obd diagnostic trouble codes
                byteIndex += 25;
            }

            // is fms journey stop data present?
            if ((report.moduleMask & AstraProtocolXFmsJourneyStopDataModule.moduleMask) == AstraProtocolXFmsJourneyStopDataModule.moduleMask)
            {
                // skip fms journey stop data
                byteIndex += 13;
            }

            // is obd journey stop data present?
            if ((report.moduleMask & AstraProtocolXObdJourneyStopDataModule.moduleMask) == AstraProtocolXObdJourneyStopDataModule.moduleMask)
            {
                // skip obd journey stop data
                byteIndex += 9;
            }

            // is carrier temperature data present?
            if ((report.moduleMask & AstraProtocolXCarrierTemperatureDataModule.moduleMask) == AstraProtocolXCarrierTemperatureDataModule.moduleMask)
            {
                // skip carrier temperature data
                byteIndex += 25;
            }

            // is 1wire temperature probe present?
            if ((report.moduleMask & AstraProtocolXOneWireTemperatureProbeModule.moduleMask) == AstraProtocolXOneWireTemperatureProbeModule.moduleMask)
            {
                // skip 1wire temperature probe
                byteIndex += 8;
            }

            // is carrier 2-way alarms present?
            if ((report.moduleMask & AstraProtocolXCarrierTwoWayAlarmsModule.moduleMask) == AstraProtocolXCarrierTwoWayAlarmsModule.moduleMask)
            {
                // skip carrier 2-way alarms
                byteIndex += 17;
            }

            // is rayvolt e-bicycle present?
            if ((report.moduleMask & AstraProtocolXRayvoltEBicycleModule.moduleMask) == AstraProtocolXRayvoltEBicycleModule.moduleMask)
            {
                // skip rayvolt e-bicycle
                byteIndex += 18;
            }

            // is econ 3 byte present?
            if ((report.moduleMask & AstraProtocolXEconThreeByteModule.moduleMask) == AstraProtocolXEconThreeByteModule.moduleMask)
            {
                // skip econ 3 byte
                byteIndex += 3;
            }

            // is gritter data present?
            if ((report.moduleMask & AstraProtocolXGritterDataModule.moduleMask) == AstraProtocolXGritterDataModule.moduleMask)
            {
                // skip gritter data
                byteIndex += 4;
            }

            // is redfordge weight present?
            if ((report.moduleMask & AstraProtocolXRedforgeWeightModule.moduleMask) == AstraProtocolXRedforgeWeightModule.moduleMask)
            {
                // skip redforge weight
                byteIndex += 6;
            }

            // is econ gritter data present?
            if ((report.moduleMask & AstraProtocolXEconGritterDataModule.moduleMask) == AstraProtocolXEconGritterDataModule.moduleMask)
            {
                // skip econ gritter data
                byteIndex += 4;
            }

            // is nmea 2000 data present?
            if ((report.moduleMask & AstraProtocolXNmea2000DataModule.moduleMask) == AstraProtocolXNmea2000DataModule.moduleMask)
            {
                // skip nmea 2000 data
                byteIndex += 73;
            }

            // is sim card subscriber id present?
            if ((report.moduleMask & AstraProtocolXSimCardSubscriberIdModule.moduleMask) == AstraProtocolXSimCardSubscriberIdModule.moduleMask)
            {
                // skip sim card subscriber id
                byteIndex += 7;
            }

            // is sim card serial number present?
            if ((report.moduleMask & AstraProtocolXSimCardSerialNumberModule.moduleMask) == AstraProtocolXSimCardSerialNumberModule.moduleMask)
            {
                // skip sim card serial number
                byteIndex += 20;
            }

            // is fms driver id present?
            if ((report.moduleMask & AstraProtocolXFmsDriverIdModule.moduleMask) == AstraProtocolXFmsDriverIdModule.moduleMask)
            {
                // skip fms driver id
                byteIndex += 38;
            }

            // is fms in-journey high-res present?
            if ((report.moduleMask & AstraProtocolXFmsInJourneyHighResModule.moduleMask) == AstraProtocolXFmsInJourneyHighResModule.moduleMask)
            {
                // skip fms in-journey high-res
                byteIndex += 20;
            }

            // is fms driver working states present?
            if ((report.moduleMask & AstraProtocolXFmsDriverWorkingStatesModule.moduleMask) == AstraProtocolXFmsDriverWorkingStatesModule.moduleMask)
            {
                // skip fms driver working states
                byteIndex += 5;
            }

            // is segway ninebot es4 sharing present?
            if ((report.moduleMask & AstraProtocolXSegwayNinebotEs4SharingModule.moduleMask) == AstraProtocolXSegwayNinebotEs4SharingModule.moduleMask)
            {
                // skip segway ninebot es4 sharing
                byteIndex += 12;
            }

            // is sensors present?
            if ((report.moduleMask & AstraProtocolXSensorsModule.moduleMask) == AstraProtocolXSensorsModule.moduleMask)
            {
                report.sensorData = new();
                for (int i = 0; i < 6; i++)
                {
                    AstraProtocolXSensor sensor = new();

                    // get sensor type
                    switch (bytes[byteIndex] & 0x0F)
                    {
                        case 1:
                            sensor.type = AstraProtocolXSensorType.TEMPERATURE;
                            break;
                        case 2:
                            sensor.type = AstraProtocolXSensorType.HUMIDITY;
                            break;
                        case 3:
                            sensor.type = AstraProtocolXSensorType.LIGHT;
                            break;
                        default:
                            sensor.type = AstraProtocolXSensorType.NOT_USED;
                            break;
                    }

                    if (sensor.type != AstraProtocolXSensorType.NOT_USED)
                    {
                        // data valid
                        if ((bytes[byteIndex] & 0x80) == 0x80)
                        {
                            sensor.dataValid = true;
                        }
                        else
                        {
                            sensor.dataValid = false;
                        }

                        byteIndex++;

                        // data
                        if (sensor.type == AstraProtocolXSensorType.TEMPERATURE)
                        {
                            short parsed = (short)Utils.parseU16At(ref bytes, byteIndex);
                            sensor.tempC = (double)parsed / 100;
                        }
                        else if (sensor.type == AstraProtocolXSensorType.HUMIDITY)
                        {
                            ushort parsed = Utils.parseU16At(ref bytes, byteIndex);
                            sensor.humidityPercent = (double)parsed / 100;
                        }
                        byteIndex += 2;
                    }
                    else
                    {
                        byteIndex += 3;
                    }


                    report.sensorData.sensors.Add(sensor);
                }
            }

            if ((report.moduleMask & AstraProtocolXBatteryUsageStatisticsModule.moduleMask) == AstraProtocolXBatteryUsageStatisticsModule.moduleMask)
            {
                report.batteryUsageStatistics = new();
                report.batteryUsageStatistics.gnssAcquiringTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.gnssFixingTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.modemOnTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.modemCsRegisteredTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.modemPsRegisteredTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.pdpActiveTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.socketOpenTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.wakeTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.sleepTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.externalPowerOnTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.bleOnTimeS = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.wakeCycleCount = Utils.parseU24(ref bytes, ref byteIndex);
                report.batteryUsageStatistics.reportsSent = Utils.parseU24(ref bytes, ref byteIndex);
            }

            // is astra reserved data present?
            if ((report.moduleMask & AstraProtocolXReservedModule.moduleMask) == AstraProtocolXReservedModule.moduleMask)
            {
                report.reservedData = new();

                ushort payloadSize = Utils.parseU16(ref bytes, ref byteIndex);
                byte payloadType = bytes[byteIndex++];

                if (payloadType == AstraProtocolXReservedModuleNVData.type)
                {
                    report.reservedData.nvData = new();

                    // skip length
                    byteIndex += 2;

                    // flags
                    report.reservedData.nvData.flags = Utils.parseU16BE(ref bytes, ref byteIndex);

                    // pc reg
                    report.reservedData.nvData.pc = Utils.parseU32BE(ref bytes, ref byteIndex);

                    // lr reg
                    report.reservedData.nvData.lr = Utils.parseU32BE(ref bytes, ref byteIndex);

                    // wdg levels
                    for (int i = 0; i < 15; i++)
                    {
                        report.reservedData.nvData.watchdogLevels[i] = Utils.parseU16BE(ref bytes, ref byteIndex);
                    }

                    // triggered wdg service name
                    report.reservedData.nvData.watchdogServiceName = "";
                    for (int i = 0; i < 8; i++)
                    {
                        report.reservedData.nvData.watchdogServiceName += (char)bytes[byteIndex++];
                    }
                }
                else
                {
                    // skip payload size
                    byteIndex += (payloadSize - 1);
                }
            }

            return report;
		}
	}
}

