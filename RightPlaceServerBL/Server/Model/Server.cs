using RightPlaceBL.Server.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RightPlaceBL.Server.Model
{
    public class Server
    {
        TcpListener tcpListener;
        Client Client { get; set; }

        public Dictionary<string, ICommand> ServerCommands = new Dictionary<string, ICommand>();

        internal List<Client> clientObjects = new List<Client>();
        internal CommandsServer CommandsServer { get; private set; }

        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Client = new Client(tcpClient);
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

        
        public void Disconnect()
        {
            tcpListener.Stop(); 

            Client.Close(); 
           
            Environment.Exit(0); 
        }
    }
}
