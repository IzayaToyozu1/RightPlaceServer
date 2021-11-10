using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string Mes { get; set; }
        public DateTime DateTimeMes { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }

        public Message()
        {

        }

        public Message(string mes, DateTime dateTimeMes, User user, Chat chat)
        {
            Mes = mes;
            DateTimeMes = dateTimeMes;
            User = user;
            Chat = chat;
        }
    }
}
