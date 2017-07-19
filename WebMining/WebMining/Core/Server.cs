using System;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebMining
{
    public class Server : IDisposable
    {
        TcpListener server;
        StreamReader reader;
        StreamWriter writer;

        Func<string, string> onReceive;

        public Action<bool> OnPauseOrResume { get; set; }
        public Action OnClientConnected { get; set; }

        private bool isConnected;
        private int _port;

        public Server(Func<string,string> onreceive, int port = 9999)
        {
            _port = port;
            onReceive = onreceive;
            isConnected = false;
            isPaused = false;
            start();
        }

        private bool isPaused;

        private void Pause()
        {
            isPaused = true;
            if (OnPauseOrResume != null)
                OnPauseOrResume(isPaused);
        }
        private void Resume()
        {
            isPaused = false;
            if (OnPauseOrResume != null)
                OnPauseOrResume(isPaused);
        }

        public Server PauseOrResume()
        {
            if (isPaused)
                Resume();
            else
                Pause();
            return this;
        }

        private void start()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (acceptClient())
                        while (isConnected)
                        {
                            send(onReceive(receive()));
                            sleepIfServerPaused();
                        }
                }
            });
        }

        private void sleepIfServerPaused()
        {
            while (isPaused)
                Thread.Sleep(500);
        }

        private bool acceptClient()
        {
            if (server != null && isConnected)
                return false;

            if (server == null)
            {
                server = new TcpListener(IPAddress.Any, _port);
                server.Start();
            }

            var client = server.AcceptTcpClient();
            if (isPaused == true)
            {
                client.Close();
                return false;
            }

            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            isConnected = true;

            if (OnClientConnected != null)
                OnClientConnected();

            return true;
        }

        private void send(string line)
        {
            if (isConnected == false || isPaused == true)
                return;

            writer.WriteLine(line);
            writer.Flush();
        }

        private string receive()
        {
            if (isConnected == false || isPaused == true)
                return "";

            string line = reader.ReadLine();
            if (line.ToLower() == "exit")
                isConnected = false;
            return line;
        }

        public void Dispose()
        {
            if(writer != null)
                writer.Dispose();
            if (reader != null)
                reader.Dispose();
            if (server != null)
                server.Stop();
        }
    }
}