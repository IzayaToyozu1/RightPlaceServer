using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using RightPlaceBL.DataBase;
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
        CommandsServer _commandsServer;
        UserMonipulator _userMonipulator;
        ChatManipulator _chatManipulator;

        public User User { get; private set; }
        public NetworkStream Stream { get; private set; }

        public Client (TcpClient client)
        {
            _client = client;
            _commandsServer = new CommandsServer(_userMonipulator, Stream);
            _userMonipulator = new UserMonipulator(User);
        }

        public void Process()
        {
            Stream = _client.GetStream();
            while (true)
            {
                string GetCommandClient = SendReceivData.GetData(Stream);
                ICommand command = _commandsServer.Commands[GetCommandClient];
                command.Execude();
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
