using System;
namespace AstraProtocolXParser.modules
{
	public class AstraProtocolXDriverBehaviourModule
	{
        public static ulong moduleMask = 1 << 4;

		public float accelXMax;
        public float accelXMin;
        public float accelYMax;
        public float accelYMin;
        public float accelZMax;
        public float accelZMin;
        public ushort idleTimeS;

        public AstraProtocolXDriverBehaviourModule()
		{
		}
	}
}

