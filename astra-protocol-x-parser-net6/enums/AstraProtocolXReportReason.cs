﻿namespace AstraProtocolXParser.enums
{
	public enum AstraProtocolXReportReason
	{
		TIMED_INTERVAL_ELAPSED = 0,
		DISTANCE_TRAVELLED_EXCEEDED = 1,
		POSITION_ON_DEMAND = 2,
		GEOFENCE = 3,
		PANIC_SWITCH_ACTIVATED = 4,
		EXTERNAL_IO = 5,
		JOURNEY_START = 6,
		JOURNEY_STOP = 7,
		HEADING_CHANGE = 8,
		LOW_BATTERY = 9,
		EXTERNAL_POWER = 10,
		IDLING_START = 11,
		IDLING_END = 12,
		IDLING_ONGOING = 13,
		REBOOT = 14,
		SPEED_OVERTHRESHOLD = 15,
		TOWING = 16,
		UNAUTHORISED_DRIVER_ALARM = 17,
		COLLISION_ALARM = 18,
		ACCEL_THRESHOLD_MAX = 19,
		CORNERING_THRESHOLD_MAX = 20,
		DECEL_THRESHOLD_MAX = 21,
		GPS_REACQUIRED = 22,
		CANBUS = 23,
		CARRIER = 24,
		TAMPER = 25,
		TOWING_END = 26,
		SERIAL_DEVICE = 27,
		LOW_EXTERNAL_VOLTAGE = 28,
		ENTERING_SLEEP_MODE = 29,
		ROLL_OVER = 30,
		MOTION_DETECTED_WHILST_PARKED = 31,
		COUNT = 32
	}
}

