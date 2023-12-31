﻿using Microsoft.AspNetCore.Mvc;

namespace ASP_SPD_222.Data.Entities
{
    public class MyFormDataBase
    {
        public Guid Id { get; set; }
        public String Firstname { get; set; } = null!;
        public String Lastname { get; set; } = null!;
        public String Tel { get; set; } = null!;
        public String Mail { get; set; } = null!;
        public DateTime MomentReg { get; set; }
    }
}
