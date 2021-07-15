using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using RightPlaceBL.DataBase;
using RightPlaceBL;
using System.Threading.Tasks;
using System.Text.Json;

namespace RightPlaceBL.Server.Model
{
    public class ServerObject
    {
        TcpListener tcpListener;
        static Thread listenChatThread;
        ClientObject ClientObject { get; set; }
        internal List<ClientObject> clientObjects = new List<ClientObject>();
        internal List<Chat> Chats = new List<Chat>();

        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ClientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(ClientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Disconnect();
            }
        }

        public void AddConnection(ClientObject client)
        {
            clientObjects.Add(client);
        }

        public void SendData(Command command, ClientObject client)
        {
            string json = JsonSerializer.Serialize<Command>(command);
            byte[] data = Encoding.Unicode.GetBytes(json);
            client.Stream.Write(data, 0, data.Length);
        }

        public void SendData(string message, ClientObject client)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            client.Stream.Write(data, 0, data.Length);
        }

        public int GenerationPort(Chat chat)
        {
            int port = 1;
            for (int i = 0; i < Chats.Count; i++)
            {
                if(port == Chats[i].Port)
                {
                    port++;
                }
            }
            return port;
        }

        

        // отключение всех клиентов
        public void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            ClientObject.Close(); //отключение клиента
           
            Environment.Exit(0); //завершение процесса
        }
    }
}
