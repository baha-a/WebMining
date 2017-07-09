using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace WebMining
{
    public class PipedServer
    {
        NamedPipeServerStream server;
        StreamReader reader;
        StreamWriter writer;

        Func<string, string> onReceive;

        string Name;
        public PipedServer(string name, Func<string,string> onreceive)
        {
            Name = name;
            onReceive = onreceive;
            start();
        }

        private void start()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    acceptClient();
                    while (server.IsConnected)
                        send(onReceive(receive()));
                }
            });
        }

        private void acceptClient()
        {
            if (server != null && server.IsConnected)
                return;

            server = new NamedPipeServerStream(Name);
            server.WaitForConnection();
            reader = new StreamReader(server);
            writer = new StreamWriter(server);
        }

        private void send(string line)
        {
            if (server.IsConnected == false)
            {
                return;
            }
            writer.WriteLine(line);
            writer.Flush();
        }

        private string receive()
        {
            string line = reader.ReadLine();
            if (line.ToLower() == "exit")
                server.Close();
            return line;
        }
    }
}