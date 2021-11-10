using RightPlaceBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Service
{
    public class UserManipulator
    {
        private User _user;

        public UserManipulator(User user)
        {
            _user = user;
        }

        public void AddFreand(User user)
        {
            _user.Freands.Add(user);
        }

        public void RemoveFreand(User userFreand)
        {
            _user.Freands.Remove(userFreand);
        }

        public List<User> GetListFreands()
        {
            return _user.Freands;
        }
    }
}
