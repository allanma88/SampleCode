using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Recv(args);
        }

        private static void Recv(string[] args)
        {
            TcpListener tcpListener = null;
            try
            {
                var port = int.Parse(args[0]);
                string address = "127.0.0.1";
                IPAddress ipAddress = IPAddress.Parse(address);
                tcpListener = new TcpListener(ipAddress, port);
                Console.WriteLine("listening from {0}:{1}", address, port);
                tcpListener.Start();
                Console.WriteLine("listenned from {0}:{1}", address, port);
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ConcurrentQueue<byte> queue = new ConcurrentQueue<byte>();
                    Tuple<TcpClient, ConcurrentQueue<byte>> pair = new Tuple<TcpClient, ConcurrentQueue<byte>>(tcpClient, queue);
                    ThreadPool.QueueUserWorkItem(RecvMsg, pair);
                    //ThreadPool.QueueUserWorkItem(ReadMsg, pair);
                }
            }
            finally
            {
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                }
            }
        }

        private static void RecvMsg(object o)
        {
            Tuple<TcpClient, ConcurrentQueue<byte>> pair = o as Tuple<TcpClient, ConcurrentQueue<byte>>;
            TcpClient tcpClient = pair.Item1;
            ConcurrentQueue<byte> queue = pair.Item2;
            try
            {
                using (var networkStream = tcpClient.GetStream())
                {
                    Console.WriteLine("connected from {0}", tcpClient.Client.RemoteEndPoint.ToString());
                    byte[] bytes = new byte[256];
                    int i;
                    while ((i = networkStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var content = Encoding.UTF8.GetString(bytes, 0, i);
                        Console.Write(content);
                        //for (int j = 0; j < i; j++)
                        //{
                        //    queue.Enqueue(bytes[j]);
                        //}
                    }
                }
            }
            finally
            {
                tcpClient.Close();
            }
        }

        private static void ReadMsg(object o)
        {
            Tuple<TcpClient, ConcurrentQueue<byte>> pair = o as Tuple<TcpClient, ConcurrentQueue<byte>>;
            TcpClient tcpClient = pair.Item1;
            ConcurrentQueue<byte> queue = pair.Item2;
            int i = 0;
            while (tcpClient.Connected)
            {
                byte b;
                while (queue.TryDequeue(out b))
                {
                    var content = Encoding.ASCII.GetString(new byte[] { b }, 0, 1);
                    Console.Write(content);
                }
            }
        }
    }
}
