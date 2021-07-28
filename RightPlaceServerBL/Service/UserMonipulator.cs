using RightPlaceBL.DataBase;
using RightPlaceBL.Model;
using System.Linq;

namespace RightPlaceBL.Service
{
    public class UserMonipulator
    {
        internal User User { get; set; }
        ChatManipulator _chatManipulator;

        public UserMonipulator(User user)
        {
            User = user;
            _chatManipulator = new ChatManipulator();
        }

        public void CreatChat(string chatName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Chat chat = new Chat();
                chat.Name = chatName;
                chat.Users.Add(User);
                db.Chats.Add(chat);
                db.SaveChanges();
            } 
        }

        public void AddUserChat(string chatName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Chat chat = db.Chats.FirstOrDefault(c => c.Name == chatName);
                chat.Name = chatName;
                chat.Users.Add(User);
                db.SaveChanges();
            }
        }

        public void DeletChat(string chatName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Chat chat = db.Chats.First(c => c.Name == chatName);
                User.Chats.Remove(chat);
                db.SaveChanges();
            }
        }

        public void Message(string chatName, string message)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Chat chat = db.Chats.FirstOrDefault(c => c.Name == chatName);
                _chatManipulator.AddMessage(message);
                db.SaveChanges();
            }
        }

        public void Authorization()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Name == User.Login && u.Password == User.Password);
                User = user;
            }
        }

        public void Registration()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Add(User);
                db.SaveChanges();
            }
        }
    }
}
