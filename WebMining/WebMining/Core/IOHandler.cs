using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace WebMining
{
    [Serializable]
    public class IOData
    {
        public IOData() { }

        public List<User> Users { get; set; }
        public List<SerializableClusterOfUsers> Clusters { get; set; }
        public List<Rule> Rules { get; set; }

        public List<string> itemKeys { get; set; }
        public List<char> itemValues { get; set; }
    }

    public static class IOHandler
    {
        public static void WriteToXmlGZIPFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));

                using (FileStream file = new FileStream(filePath, (append ? FileMode.Append : FileMode.Create), FileAccess.Write))
                {
                    using (var gzipStream = new GZipStream(file, CompressionMode.Compress))
                    {
                        serializer.Serialize(gzipStream, objectToWrite);
                    }
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        public static T ReadFromXmlGZIPFile<T>(string filePath) where T : new()
        {
            FileStream reader = null;
            GZipStream gzip = null;
            try
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(
                    gzip = new GZipStream(reader = new FileStream(filePath,FileMode.Open), CompressionMode.Decompress));
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (gzip != null)
                    gzip.Close();
            }
        }

        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader = new StreamReader(filePath));
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}