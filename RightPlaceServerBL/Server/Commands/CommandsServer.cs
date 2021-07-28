using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RightPlaceBL.Model;
using RightPlaceBL.Service;

namespace RightPlaceBL.Server.Commands
{
    internal class CommandsServer
    {

        UserMonipulator _userMonipulator;
        public Dictionary<string, ICommand> Commands { get; } = new Dictionary<string, ICommand>();
        
        internal CommandsServer(UserMonipulator userMonipulator, NetworkStream stream)
        {
            Commands.Add("Authentication", new AuthenticationCommand(userMonipulator.User, stream));
            Commands.Add("Registration", new RegistrationCommand(userMonipulator.User, stream));
        }
    }
}
