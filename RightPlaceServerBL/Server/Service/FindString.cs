using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RightPlaceBL.Server.Service
{
    public static class FindString
    {
        public static string GetString(string data, string pattern)
        {
            Regex r = new Regex(pattern);
            string result = r.Match(data).ToString();
            data = r.Replace(data, result);
            return result;
        }
    }
}
