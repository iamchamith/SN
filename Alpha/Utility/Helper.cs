using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Utility
{
    public class Helper
    {
        public static string GenarateRandomNumber(int length) {
            Random random = new Random();
                 const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}