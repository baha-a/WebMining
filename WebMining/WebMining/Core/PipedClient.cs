using System.IO;
using System.IO.Pipes;

namespace WebMining
{
    class PipeClient
    {
        StreamWriter writer;
        StreamReader reader;
        NamedPipeClientStream client;
        bool isClosed;
        public PipeClient(string name)
        {
            open(name);
        }

        public PipeClient open(string name)
        {
            client = new NamedPipeClientStream(name);
            client.Connect();
            reader = new StreamReader(client);
            writer = new StreamWriter(client);

            isClosed = false;

            return this;
        }


        public PipeClient send(string msg)
        {
            if (isClosed)
                return this;
            writer.WriteLine(msg);
            writer.Flush();

            return this;
        }

        public string receive()
        {
            if (isClosed)
                return "";
            return reader.ReadLine();
        }

        public PipeClient close()
        {
            isClosed = true;
            writer.Close();
            reader.Close();
            client.Close();

            return this;
        }
    }
}
