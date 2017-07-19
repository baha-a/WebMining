using System;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;

namespace WebMining
{
    class Client : IDisposable
    {
        StreamWriter writer;
        StreamReader reader;
        TcpClient client;

        bool isClosed;
        public Client(int port) : this("127.0.0.1", port) { }
        public Client(string ip, int port)
        {
            client = new TcpClient() { SendTimeout = 2000, ReceiveTimeout = 2000 };
            client.Connect(IPAddress.Parse(ip), port);
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());

            isClosed = false;
        }


        public void send(string msg)
        {
            if (isClosed)
                return;
            writer.WriteLine(msg);
            writer.Flush();
        }

        public string receive()
        {
            if (isClosed)
                return "";
            return reader.ReadLine();
        }

        public string command(string c)
        {
            send(c);
            return receive();
        }

        public Client close()
        {
            command("exit");

            isClosed = true;

            writer.Close();
            reader.Close();
            client.Close();

            return this;
        }

        public void Dispose()
        {
            close();
        }
    }
}
