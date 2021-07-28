using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Model
{
    public class ListMessage
    {
        public List<string> Messages = new List<string>();

        public string[] GetListMessages()
        {
            return Messages.ToArray();
        }
    }
}
