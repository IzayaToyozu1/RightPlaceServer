using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public Chat() { }
        public Chat(string name)
        {
            Name = name;
        }
    }
}
