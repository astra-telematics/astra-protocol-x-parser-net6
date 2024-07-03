# astra-protocol-x-parser-net6
An official lightweight .NET package for parsing data from Astra Telematics IoT devices.

This package is maintained by Astra Telematics and is currently up-to-date with Protocol X V2.8 although only supports the selected modules listed below.

#### Supported Data Modules
- [x] Device Power
- [x] GPS Data
- [x] Digital I/O
- [x] Analogue Inputs
- [x] Driver Behaviour
- [x] Driver ID
- [x] Signal Quality
- [x] GSM Network Info
- [x] Geofences
- [x] Temperature + Humidity Sensors
- [x] Astra Generic Debug Data

###### Please contact support@astratelematics.com if you would like support for a different module added

### Installation
Available for easy installatio via Nuget, just search for "AstraProtocolXParser"

### Basic example
##### This is a simple barebones .NET TCP server and is not meant for production use

```cs
using System.Net.Sockets;
using AstraProtocolXParser;

TcpListener tcpListener = new TcpListener(System.Net.IPAddress.Any, 2010);
List<Client> tcpClients = new List<Client>();

tcpListener.Start();

Console.WriteLine("started, listening on port 2010...");

for (;;)
{
    Client? newClient = null;
    if (tcpListener.Pending())
    {
        Console.WriteLine("new connection, accepting...");
        newClient = new Client(tcpListener.AcceptTcpClient());
        new Thread(new ThreadStart(newClient.run)).Start();
        tcpClients.Add(newClient);
    }

    Thread.Sleep(100);
}

class Client
{
    public TcpClient socket;
    public byte[] buffer = new byte[2048];
    public int bufferIndex = 0;
    public ManualResetEvent readResetEvent = new(false);

    public Client(TcpClient socket)
    {
        this.socket = socket;
    }

    public async void run()
    {
        int i;
        byte[] ackBuffer = { 0x06 };

        while (socket.Connected)
        {
            try
            {
                socket.GetStream().BeginRead(buffer, 0, buffer.Length, readCallback, this);

                if (readResetEvent.WaitOne(1000, false))
                {
                    readResetEvent.Reset();

                    Console.WriteLine($"{bufferIndex} bytes rxd");

                    string? error = null;
                    AstraProtocolXPacket? packet = AstraProtocolXPacket.fromBytes(buffer, bufferIndex, ref error);

                    if (packet != null)
                    {
                        if (packet.isLogin && packet.deviceData != null)
                        {
                            Console.WriteLine($"device logged in imei:{packet.deviceData.imei}, model:{packet.deviceData.model}, fw:{packet.deviceData.firmwareVersion}, csum:{packet.deviceData.settingsChecksum}");
                        }
                        else if (packet.reports.Count > 0)
                        {
                            Console.WriteLine($"{packet.reports.Count} reports parsed");
                            for (i = 0; i < packet.reports.Count; i++)
                            {
                                Console.WriteLine($" > sequence:{packet.reports[i].sequenceNumber}");

                                if (packet.reports[i].gpsData != null)
                                {
                                    Console.WriteLine($"   latitude:{packet.reports[i].gpsData!.latitude}, longitude: {packet.reports[i].gpsData!.longitude}");
                                }
                            }
                        }
                    }

                    if (error != null)
                    {
                        Console.WriteLine($"error parsing: {error}");
                    }

                    // ack
                    try
                    {
                        await socket.GetStream().WriteAsync(ackBuffer, 0, ackBuffer.Length);
                        await socket.GetStream().FlushAsync();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        Console.WriteLine("disconnected");
    }

    private void readCallback(IAsyncResult ar)
    {
        Client? _client = ar.AsyncState as Client;

        try
        {
            if (_client != null)
            {
                if (_client.socket != null)
                {
                    _client.bufferIndex = _client.socket.GetStream().EndRead(ar);
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            if (_client != null)
            {
                _client.readResetEvent.Set();
            }
        }
    }
}
```