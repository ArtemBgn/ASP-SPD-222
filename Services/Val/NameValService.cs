using System.Runtime.CompilerServices;

namespace ASP_SPD_222.Services.Val
{
    public class NameValService : IValService
    {
        public bool ValString(string input)
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
    }
}
