using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RightPlaceBL.Service;
using RightPlaceBL.Model;

namespace RightPlaceBL.Server.Commands
{
    public class CreatChatCommand : ICommand
    {
        User _user;
        NetworkStream _stream;

        CreatChatCommand(User user, NetworkStream stream)
        {
            _user = user;
            _stream = stream;
        }

        public void Execude()
        {
            
        }
    }
}
