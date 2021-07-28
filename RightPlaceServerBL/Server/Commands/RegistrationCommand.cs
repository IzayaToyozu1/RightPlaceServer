using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RightPlaceBL.Model;
using RightPlaceBL.Server.Service;
using RightPlaceBL.Service;


namespace RightPlaceBL.Server.Commands
{
    public class RegistrationCommand : ICommand
    {
        UserMonipulator _userMonipulator;
        NetworkStream _stream;

        public RegistrationCommand(UserMonipulator userMonipulator, NetworkStream stream)
        {
            _userMonipulator = userMonipulator;
            _stream = stream;
        }
        public void Execude()
        {
            _userMonipulator.User = SendReceivData.GetData<User>(_stream);
            _userMonipulator.Registration();
            string commandClient = "LoginTrue";
            SendReceivData.SendData(_stream, commandClient);
            SendReceivData.SendData<User>(_stream, _userMonipulator.User);
        }
    }
}
