using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RightPlaceBL.Server.Model
{
    public class Chat
    {
        
        private TcpListener _tcpListener;

        public string Name { get; set; }
        public int id { get; set; }
        public int Port { get; set; }

        private List<ClientChat> _clients = new List<ClientChat>();
       
        public void ListenerChat()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Any, Port);
                _tcpListener.Start();

                while (true)
                {
                    TcpClient tcpClient = _tcpListener.AcceptTcpClient();

                    ClientChat client = new ClientChat(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(client.ProcessChat));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Disconnect();
            }
        } 

        public void AddClient(ClientChat client)
        {
            _clients.Add(client);
        }


        public void SentMessage(string message, ClientChat client)
        {
            byte[] data = Encoding.Unicode.GetBytes(client.User.Name + ": " + message);
            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i] != client)
                {
                    _clients[i].Stream.Write(data, 0, data.Length);
                }
            }
        }

        public void Disconnect()
        {

        }
    }
}
