using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text.Json;

namespace RightPlaceBL.Server.Service
{
    public class SendReceivData
    {
        public static void SendData(NetworkStream stream, string obj)
        {
            byte[] data = Encoding.Unicode.GetBytes(obj);
            stream.Write(data, 0, data.Length);
        }

        public static void SendData<T>(NetworkStream stream, T obj)
        {
            string json = JsonSerializer.Serialize<T>(obj);
            SendData(stream, json);
        }

        public static string GetData(NetworkStream stream)
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

        public static T GetData<T>(NetworkStream stream)
        {
            string obj = GetData(stream);
            return JsonSerializer.Deserialize<T>(obj);
        }
    }
}
