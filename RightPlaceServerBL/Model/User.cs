using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        //public string Login { get; set; }
        public string Name { get; set; }
        public int Age
        {
            get
            {
                return BirthDate.Year;
            }
        }
        public DateTime BirthDate { get; private set; }

        public string Password { get; set; }
        //public string Email { get; set; }
        public User() { }
        public User(string name, DateTime birthDate, string password)
        {
            Name = name;
            BirthDate = birthDate;
            Password = password;
        }
    }
}
