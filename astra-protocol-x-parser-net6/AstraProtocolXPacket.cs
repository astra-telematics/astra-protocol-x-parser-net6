namespace AstraProtocolXParser
{
    public class AstraProtocolXPacket
    {
        public bool isLogin = false;
        public AstraDeviceData? deviceData;

        // Packet header
        public byte protocolId;
        public ushort packetLength;
        public int numReports;

        public List<AstraProtocolXReport> reports = new List<AstraProtocolXReport>();

        public AstraProtocolXPacket()
        {
        }

        public static AstraProtocolXPacket? fromBytes(byte[] bytes, int bytesLength, ref string error)
        {
            AstraProtocolXPacket packet = new();
            string? decodedPacket;
            int byteIndex = 0;

            // look for login
            try
            {
                decodedPacket = System.Text.Encoding.ASCII.GetString(bytes);

                if (decodedPacket != null)
                {
                    decodedPacket = decodedPacket.Replace("\r", "").Replace("\n", "").Replace("\0", "");

                    if (decodedPacket.StartsWith("$ASTRA;"))
                    {
                        string[] loginComponents = decodedPacket.Split(";");

                        if (loginComponents.Length >= 7)
                        {
                            packet.deviceData = new();
                            packet.deviceData.model = loginComponents[1];
                            packet.deviceData.imei = loginComponents[2];
                            packet.deviceData.vin = loginComponents[3];
                            packet.deviceData.firmwareVersion = loginComponents[4];
                            packet.deviceData.hardwareRevision = loginComponents[6];

                            if (loginComponents.Length >= 8)
                            {
                                packet.deviceData.settingsChecksum = loginComponents[7];
                            }

                            packet.isLogin = true;
                            return packet;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            // Check we have at least the packet header (4 bytes)
            if (bytesLength < 4)
            {
                error = "not enough bytes to parse packet header";
                return null;
            }
            // Packet header (mode 6)
            packet.protocolId = bytes[byteIndex++];
            packet.packetLength = Utils.parseU16(ref bytes, ref byteIndex);
            packet.numReports = bytes[byteIndex++];

            // Confirm packet length
            if (packet.packetLength != bytesLength)
            {
                error = $"packet length expected: {packet.packetLength}, rxd: {bytesLength}";
                return null;
            }

            // Confirm CRC
            ushort packetCrc = Utils.parseU16At(ref bytes, bytesLength - 2);
            if (packetCrc != Utils.astraCrc16(ref bytes, bytesLength-2))
            {
                error = "invalid checksum";
                return null;
            }

            // Are there any reports?
            if (packet.numReports <= 0)
            {
                error = "zero reports in packet";
                return null;
            }

            // Parse the reports
            for (int i = 0; i < packet.numReports; i++)
            {
                AstraProtocolXReport? report = AstraProtocolXReport.fromBytes(bytes, bytesLength, ref byteIndex, ref error);
                if (report != null)
                {
                    packet.reports.Add(report);
                }
                else
                {
                    error = $"error parsing report at index {i} ({error})";
                }
            }

            if (byteIndex != bytesLength-2)
            {
                error = $"didn't consume all bytes of packet ({byteIndex}/{bytesLength - 2})";
                return null;
            }

            return packet;
        }
    }
}

