using RightPlaceBL.Model;
using RightPlaceBL.Server.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Server.Commands
{
    public class SendMessageChat : Commands
    {
        private User _user;
        public SendMessageChat(NetworkStream stream, User user) : base(stream)
        {
            _user = user;
        }

        public override void Execude(string data)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                string nameChat = FindString.GetString(data, @"\s\w+\s");
                Chat chat = db.Chats.FirstOrDefault(c => c.Name == nameChat);
            }
        }
    }
}
