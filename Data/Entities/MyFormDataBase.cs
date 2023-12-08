using Microsoft.AspNetCore.Mvc;

namespace ASP_SPD_222.Data.Entities
{
    public class MyFormDataBase
    {
        public Guid Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Tel { get; set; }
        public String Mail { get; set; }
        public DateTime MomentReg { get; set; }
    }
}
