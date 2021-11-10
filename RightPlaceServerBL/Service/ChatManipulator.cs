using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightPlaceBL.Model;

namespace RightPlaceBL.Service
{
    public class ChatManipulator
    {
        public event Action<string> NewMessage;
        public Chat Chat {get; set;}

        public ChatManipulator()
        {
            
        }

        public void AddMessage(string message)
        {
            
        }
    }
}
