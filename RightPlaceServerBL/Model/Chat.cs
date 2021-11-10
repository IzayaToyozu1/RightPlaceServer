using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Model
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Message { get; set; }
        public Chat() { }
        public Chat(string name)
        {
            Name = name;
            Users = new List<User>();
            Message = new List<Message>();
        }
    }
}
