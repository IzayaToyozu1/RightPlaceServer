using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Sockets;
using RightPlaceBL.Server.Service;
using RightPlaceBL.Model;
using RightPlaceBL.Server.Model;

namespace RightPlaceBL.Server.Commands
{
    public class Login : Commands
    {
        User _user;
        public Login(NetworkStream stream, User user) : base(stream)
        {
            _user = user;
        }

        public void Execude(string data)
        {
            string login = FindString.GetString(data, @"\s\w+\s");
            string password = data;
            using(ApplicationContext db = new ApplicationContext())
            {
                foreach (var user in db.Users)
                {
                    if (user.Login == login && user.Password == password)
                    {
                        User userLog = user;
                        _user = user;
                        userLog.Password = null;
                        SendReceivData.SendData<User>(_stream, userLog);
                    }
                }
                SendReceivData.SendData(_stream, "%ErrorLogin% Логин или пароль введены неверно");
            }
        }
    }
}
