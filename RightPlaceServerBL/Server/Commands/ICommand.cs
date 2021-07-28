using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RightPlaceBL.Model;
using RightPlaceBL.Service;
using RightPlaceBL.Server.Service;

namespace RightPlaceBL.Server.Commands
{
    public interface ICommand
    {
        
        public void Execude();
    }
}
