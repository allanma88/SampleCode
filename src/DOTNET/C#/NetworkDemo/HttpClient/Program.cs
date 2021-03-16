using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Send(args);
        }
        
        private static void Send(string[] args)
        {
            var host = args[0];
            var port = int.Parse(args[1]);
            TcpClient tcpClient = null;
            try
            {
                tcpClient = new TcpClient();
                Console.WriteLine("connecting to {0}:{1}", host, port);
                tcpClient.Connect(host, port);
                Console.WriteLine("connected to {0}:{1}", host, port);
                using (var networkStream = tcpClient.GetStream())
                {
                    string line = null;
                    while ((line = Console.ReadLine()) != null)
                    {
                        if (line == "exit" || line == "quit")
                        {
                            break;
                        }
                        else if (line.StartsWith("upload"))
                        {
                            var filePath = line.Substring(7);
                            using (FileStream stream = new FileStream(filePath, FileMode.Open))
                            {
                                stream.CopyTo(networkStream);
                            }
                        }
                        else
                        {
                            var bytes = Encoding.UTF8.GetBytes(line + "\n");
                            networkStream.WriteAsync(bytes, 0, bytes.Length).Wait();
                            Console.WriteLine("send: " + line);
                        }
                    }
                }
            }
            finally
            {
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
            }
        }
    }
}