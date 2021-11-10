using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Model
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Chat> Chats { get; set; }
        public List<User> Freands { get; set; }

        public List<Message> Messages {get; set;}

        public User() { }

        public User(string name, DateTime birthDate, string password, string email)
        {
            Name = name;
            BirthDate = birthDate;
            Password = password;
            Email = email;
            Chats = new List<Chat>();
            Freands = new List<User>();
            Messages = new List<Message>();
        }
    }
}
