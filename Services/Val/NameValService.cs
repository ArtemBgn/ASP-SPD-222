using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ASP_SPD_222.Services.Val
{
    public class NameValService : IValService
    {
        public bool ValNameString(string input)
        {
            bool res = true;
            char[] tmp = input.ToUpper().ToCharArray();
            string abets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ'АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩьЮЯ";
            for (int i = 0; i < tmp.Length; i++)
            {
                if ( (!abets.Contains(tmp[i])) || (tmp.Length < 2) )
                {
                    res = false;
                }
            }
            return res;
        }
        public bool ValTelString(String input)
        {
            return Regex.Match(input, @"^\+380+([0-9]{9})+$").Success;
        }
        public bool ValMailString(String input)
        {
            return Regex.Match(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$").Success;
        }
    }
}
