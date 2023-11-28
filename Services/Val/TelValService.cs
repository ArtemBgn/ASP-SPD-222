using System.Text.RegularExpressions;

namespace ASP_SPD_222.Services.Val
{
    public class TelValService : IValService
    {
        public bool ValString(string input)
        {
            return Regex.Match(input, @"^\+380+([0-9]{9})+$").Success;
        }
    }
}
