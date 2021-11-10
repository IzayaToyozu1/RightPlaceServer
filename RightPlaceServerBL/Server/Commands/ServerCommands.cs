using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RightPlaceBL.Server.Service;
using RightPlaceBL.Server.Model;
using RightPlaceBL.Model;

namespace RightPlaceBL.Server.Commands
{
    public class ServerCommands
    {
        private Dictionary<string ,Commands> _commandsDict;
        private NetworkStream _stream;
        private User _user;

        public ServerCommands(NetworkStream stream, User user)
        {
            _stream = stream;
            _user = user; 
            _commandsDict = new Dictionary<string, Commands>()
            {
                ["%Registr%"] = new Registration(stream),
                ["%Login%"] = new Login(stream, user),
                ["%SendMessageChat%"] = new SendMessageChat(stream, user)
            };
        }

        public void Execude(string data)
        {
            string command = FindString.GetString(data, @"%\w+%");
            _commandsDict[command].Execude(data);
        }
    }
}
