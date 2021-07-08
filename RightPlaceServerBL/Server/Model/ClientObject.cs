using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using RightPlaceBL.DataBase;
using RightPlaceBL.Service;
using System.Collections.Generic;

namespace RightPlaceBL.Server.Model
{
    public class ClientObject
    {
        User _user;
        internal NetworkStream Stream { get; private set; }
        TcpClient _client;
        ServerObject _server;
        ApplicationContext _dataBase;
        Chat _chat;

        public ClientObject (TcpClient client, ServerObject server, ApplicationContext dataBase)
        {
            _client = client;
            _server = server;
            _dataBase = dataBase;
            _server.AddConnection(this);
        }
        public void Process()
        {
            Stream = _client.GetStream();
            while (true) 
            { 
            Command command = GetCommand();
                switch (command)
                {
                    case Command.authentication:
                        command = Command.authentication;
                        _server.SendData(command, this);
                        _user = GetDataUser();
                        Authentication(_user.Name, _user.Password);
                        break;

                    case Command.registration:
                        command = Command.registration;
                        _server.SendData(command, this);
                        _user = GetDataUser();
                        _dataBase.Users.Add(_user);
                        _dataBase.SaveChanges();
                        break;

                    case Command.createChat:
                        _server.SendData(command, this);
                        string nameChat = GetDataStream();
                        int port = _server.CreatChat(nameChat);
                        _server.SendData(port.ToString(), this);
                        break;

                    case Command.addChat:
                        _server.SendData(command, this);
                        string chatName = GetDataStream();
                        _chat = _server.Chats.FirstOrDefault(c => c.Name == chatName);
                        if (_chat == null) 
                        {
                            _server.SendData("Чат не сущ.", this);
                            break;
                        }
                        _server.SendData(_chat.Port.ToString(), this);
                        break;

                    case Command.leaveChat:

                        break;
                }
            }
        }

        private Command GetCommand() //вынести
        {
            return ServerGetSet<Command>.GetData(Stream);
        }

        public User GetDataUser() //вынести
        {
            return ServerGetSet<User>.GetData(Stream);
        }

        public string GetDataStream()
        {
            return ServerGetSet<string>.GetDataStream(Stream);
        }

        private bool Authentication(string name, string password)
        {
            _user = _dataBase.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
            if (_user == null)
            {
                _server.SendData(Command.notUser, this);
                return false;
            }
            _server.SendData(Command.okUser, this);
            return true;
        }


        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (_client != null)
                _client.Close();
        }
    }

    public enum Command 
    {
        authentication,
        notUser,
        okUser,
        registration,
        createChat,
        addChat,
        leaveChat
    }
}
