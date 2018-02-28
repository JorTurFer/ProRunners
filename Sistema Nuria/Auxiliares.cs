using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Nuria
{
    static class Auxiliares
    {
        public static string RebuildName(string strInput)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in strInput.Split(' '))
            {
                string tmp = item.ToLower();
                sb.Append(tmp.First().ToString().ToUpper() + tmp.Substring(1) + " ");
            }

            return sb.ToString().TrimEnd();

        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}
