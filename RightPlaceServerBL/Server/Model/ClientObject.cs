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
        public User User { get; private set; }
        internal NetworkStream Stream { get; private set; }
        TcpClient _client;
        ServerObject _server;
        string _message;

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
                        _server.SentData(command, this);
                        User = GetDataUser();
                        Authentication(User.Login, User.Password);
                        break;

                    case Command.registration:
                        command = Command.registration;
                        _server.SentData(command, this);
                        User = GetDataUser();
                        using(ApplicationContext db = new ApplicationContext())
                        {
                            db.Users.Add(User);
                            db.SaveChanges();
                        }
                        break;

                    case Command.createChat:
                        _server.SentData(command, this);
                        string nameChat = GetDataStream();
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            Chat chat = new Chat() {Name = nameChat };
                            db.Chats.Add(chat);
                            User.Chats.Add(chat);
                            db.SaveChanges();
                        }
                        break;

                    case Command.addChat:
                        _server.SentData(command, this);
                        string chatName = GetDataStream();
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            Chat chat = db.Chats.FirstOrDefault(c => c.Name == chatName);
                            User.Chats.Add(chat);
                            db.SaveChanges();
                        }
                        break;

                    case Command.leaveChat:
                        _server.SentData(command, this);
                        chatName = GetDataStream();
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            Chat chat = db.Chats.FirstOrDefault(c => c.Name == chatName);
                            User.Chats.Remove(chat);
                            db.SaveChanges();
                        }
                        break;

                    case Command.messageChat:
                        _server.SentData(command, this);
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            Chat chat = ServerGetSet<Chat>.GetData(Stream);
                            _message = ServerGetSet<string>.GetDataStream(Stream);
                            foreach(var user in chat.Users)
                            {
                                var client = _server.clientObjects.First(c => c.User == user);
                                ServerGetSet<Command>.SentDataStrem(client.Stream, Command.getMessageChat);
                                ServerGetSet<string>.SentString(chat.Name, client.Stream);
                                ServerGetSet<string>.SentString(_message, client.Stream);
                            }
                        }
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
                User = db.Users.FirstOrDefault(u => u.Name == login && u.Password == password);
                if (User == null)
                {
                    _server.SentData(Command.notUser, this);
                    return;
                }
                _server.SentData(Command.okUser, this);
                ServerGetSet<List<Chat>>.SentDataStrem(Stream, User.Chats);
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
