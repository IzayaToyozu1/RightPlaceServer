using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RightPlaceBL.Server.Service;
using RightPlaceBL.Model;
using RightPlaceBL.Service;

namespace RightPlaceBL.Server.Commands
{
    public class AuthenticationCommand : ICommand
    {
        UserMonipulator _userMonipulator;
        NetworkStream _stream;

        public AuthenticationCommand(UserMonipulator userMonipulator, NetworkStream stream)
        {
            _userMonipulator = userMonipulator;
            _stream = stream;
        }

        public void Execude()
        {
            User user = SendReceivData.GetData<User>(_stream);
            _userMonipulator.Authorization();
            string commandClient = _userMonipulator.User == null ? "LoginFalse" : "LoginTrue";
            SendReceivData.SendData(_stream, commandClient);
            SendReceivData.SendData<User>(_stream, _userMonipulator.User);
        }
    }
}
