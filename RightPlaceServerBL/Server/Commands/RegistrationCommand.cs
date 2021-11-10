using RightPlaceBL.Model;
using RightPlaceBL.Server.Service;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RightPlaceBL.Server.Commands
{
    public class RegistrationCommand : Commands
    {
        const string PATTERN_EMAIL = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public RegistrationCommand(NetworkStream stream) : base(stream)
        {
        }

        public void Execude(string data)
        {
            string dataSend = "%Registr%";
            string errorSend = "ErrorReg";
            User user = JsonSerializer.Deserialize<User>(data);
            if (Regex.IsMatch(user.Email, PATTERN_EMAIL, RegexOptions.IgnoreCase))
            {
                SendReceivData.SendData(_stream, errorSend+ " неверно введен Email");
                return;
            }
            using(ApplicationContext db = new ApplicationContext())
            {
                foreach(var u in db.Users)
                {
                    if(user.Login == u.Login)
                    {
                        dataSend = "%ErrorReg";
                        SendReceivData.SendData(_stream, errorSend + "логин занят");
                        return;
                    }
                }
                db.Users.Add(user);
                db.SaveChanges();
            }
            SendReceivData.SendData(_stream, dataSend + "Аккаунт создан");
        }
    }
}
