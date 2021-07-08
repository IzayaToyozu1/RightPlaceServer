using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using RightPlaceBL.Service;

namespace RightPlaceBL.Server.Model
{
    public class ClientChat
    {
        List<Chat> chats = new List<Chat>();
        TcpClient _client;
        Chat _chat;
        internal User User { get; private set; } 
        internal NetworkStream Stream { get; set; }


        public ClientChat(TcpClient client, Chat chat)
        {
            _client = client;
            _chat = chat;
            _chat.AddClient(this);
        }
        public void ProcessChat()
        {
            try
            {
                Stream = _client.GetStream();
                string message;
                User = ServerGetSet<User>.GetData(Stream);
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    try
                    {
                        message = ServerGetSet<string>.GetDataStream(Stream);
                        Console.WriteLine(message + _chat.Name + _chat.Port);
                        _chat.SentMessage(message, this);
                    }
                    catch
                    {
                        message = " покинул чат";
                        _chat.SentMessage(message, this);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // в случае выхода из цикла закрываем ресурсы
                Close();
            }
        }
        private void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (_client != null)
                _client.Close();
        }
    }
}
