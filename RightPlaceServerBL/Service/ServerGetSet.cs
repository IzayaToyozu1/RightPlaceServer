using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text.Json;

namespace RightPlaceBL.Service
{
    public static class ServerGetSet<T>
    {
        public static T GetData(NetworkStream stream)
        {
            return JsonSerializer.Deserialize<T>(GetDataStream(stream));
        }
        public static void SentDataStrem(NetworkStream stream, T obj)
        {
            string json = JsonSerializer.Serialize<T>(obj);
            byte[] data = Encoding.Unicode.GetBytes(json);
            stream.Write(data, 0, data.Length);
        }

        public static void SentString(string message, NetworkStream stream)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        public static string GetDataStream(NetworkStream stream)
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            return builder.ToString();
        }
    }
}
