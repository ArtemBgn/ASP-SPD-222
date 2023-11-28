using System.Text.RegularExpressions;

namespace ASP_SPD_222.Services.Val
{
    public class MailValService : IValService
    {
        public bool ValString(string input)
        {
            return Regex.Match(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$").Success;
        }
    }
}
