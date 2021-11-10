using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using RightPlaceBL.Service;
using System.Collections.Generic;
using RightPlaceBL.Server.Commands;
using RightPlaceBL.Server.Service;
using RightPlaceBL.Model;

namespace RightPlaceBL.Server.Model
{
    public class Client
    {
        TcpClient _client;
        UserManipulator _userMonipulator;
        ChatManipulator _chatManipulator;
        Server _server;
        ServerCommands _commands;

        public User User { get; private set; }
        public NetworkStream Stream { get; private set; }

        public Client (TcpClient client, Server server)
        {
            _client = client;
            _server = server;
            server.AddConnection(this);
            _commands = new ServerCommands(Stream, User);
        }

        public void Process()
        {
            try 
            { 
                Stream = _client.GetStream();
                while (true)
                {
                    string GetCommandClient = SendReceivData.GetData(Stream);
                    _commands.Execude(GetCommandClient);
                }
            }
            catch(Exception e)
            {
                _server.RemoveConection(User.Id);
            }
        }

        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (_client != null)
                _client.Close();
        }
    }
}
