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

        public ClientObject (TcpClient client, ServerObject server)
        {
            _client = client;
            _server = server;
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
                        Authentication(_user.Login, _user.Password);
                        break;

                    case Command.registration:
                        command = Command.registration;
                        _server.SendData(command, this);
                        _user = GetDataUser();
                        using(ApplicationContext db = new ApplicationContext())
                        {
                            db.Users.Add(_user);
                            db.SaveChanges();
                        }
                        break;

                    case Command.createChat:
                        _server.SendData(command, this);
                        string nameChat = GetDataStream();

                        break;

                    case Command.addChat:
                        _server.SendData(command, this);
                        string chatName = GetDataStream();
                        
                        break;

                    case Command.leaveChat:

                        break;

                    case Command.messageChat:

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

        private void Authentication(string login, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                _user = db.Users.FirstOrDefault(u => u.Name == login && u.Password == password);
                if (_user == null)
                {
                    _server.SendData(Command.notUser, this);
                    return;
                }
                _server.SendData(Command.okUser, this);
                ServerGetSet<List<Chat>>.SentDataStrem(Stream, _user.Chats);
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

    public enum Command 
    {
        authentication,
        notUser,
        okUser,
        registration,
        createChat,
        addChat,
        leaveChat,
        messageChat,
        getMessageChat
    }
}
