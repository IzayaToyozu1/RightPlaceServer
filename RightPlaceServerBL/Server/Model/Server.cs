using RightPlaceBL.Server.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;

namespace RightPlaceBL.Server.Model
{
    public class Server
    {
        private const string _ip = "187.135.115.246";
        private const string _port = "8888";

        TcpListener tcpListener;
        List<Client> clientObjects = new List<Client>();
 
        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Client Client = new Client(tcpClient, this);
                    Task task = new Task(Client.Process);
                    task.Start();
                }
            }
            catch (Exception ex)
            {
                Disconnect();
            }
        }

        public void AddConnection(Client client)
        {
            clientObjects.Add(client);
        }

        public void RemoveConection(int Id)
        {
            Client client = clientObjects.FirstOrDefault(u => u.User.Id == Id);
            if (client != null)
                clientObjects.Remove(client);
        }

        public void Disconnect()
        {
            tcpListener.Stop();
            foreach (var client in clientObjects)
                client.Close();
            Environment.Exit(0); 
        }
    }
}
