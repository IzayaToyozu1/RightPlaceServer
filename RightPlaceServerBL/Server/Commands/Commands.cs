using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace RightPlaceBL.Server.Commands
{
    public abstract class Commands
    {
        internal NetworkStream _stream;
        public Commands(NetworkStream stream)
        {
            _stream = stream;
        }
        public virtual void Execude(string data)
        {

        }
    }
}
