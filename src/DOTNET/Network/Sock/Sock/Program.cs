using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sock
{
    class Program
    {
        private static bool _isServer;
        private static bool _isSourceClient;
        private static int _serverPort;
        private static IPAddress _serverIp;
        private static int _localPort;

        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                IPAddress serverIp;
                int serverPort;
                if (arg == "-s")
                {
                    _isServer = true;
                }
                else if (arg == "-i")
                {
                    _isSourceClient = true;
                }
                else if (arg.StartsWith("-b"))
                {
                    _localPort = int.Parse(arg.Substring(2));
                }
                else if (int.TryParse(arg, out serverPort))
                {
                    _serverPort = serverPort;
                }
                else if (IPAddress.TryParse(arg, out serverIp))
                {
                    _serverIp = serverIp;
                }
            }

            if (_isServer)
            {
                StartTcpServer();
            }
            else if (_isSourceClient)
            {
                StartTcpClient();
            }
        }

        private static void StartTcpServer()
        {
            TcpListener server = null;
            try
            {
                if (_serverIp == null)
                {
                    _serverIp = new IPAddress(0);
                }
                server = new TcpListener(_serverIp, _serverPort);
                server.Start();
                Console.WriteLine("Server started");
                while (true)
                {
                    Console.WriteLine("Waiting for a connection in {0}:{1}... ", _serverIp, _serverPort);
                    TcpClient client = server.AcceptTcpClientAsync().Result;
                    IPEndPoint ipEndPoint = client.Client.LocalEndPoint as IPEndPoint;
                    Console.WriteLine("Connection on {0}.{1} from {2}.{3}", _serverIp, _serverPort, ipEndPoint.Address, ipEndPoint.Port);
                    NetworkStream stream = client.GetStream();
                    Byte[] bytes = new Byte[256];
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        string data = Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        byte[] msg = Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                    client.Dispose();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }
        }

        private static void StartTcpClient()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Client.Bind(new IPEndPoint(new IPAddress(0), _localPort));
                    client.ConnectAsync(_serverIp, _serverPort).Wait();
                    IPEndPoint ipEndPoint = client.Client.LocalEndPoint as IPEndPoint;
                    Console.WriteLine("connected on {0}.{1} to {2}.{3}", ipEndPoint.Address, ipEndPoint.Port, _serverIp, _serverPort);
                    using (var stream = client.GetStream())
                    {
                        string message = "aaaa";
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("Sent: {0}", message);
                        data = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine("Received: {0}", responseData);
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }
}