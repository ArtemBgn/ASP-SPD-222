using Microsoft.AspNetCore.Mvc;

namespace ASP_SPD_222.Models.Home
{
    public class MyFormDataBaseFormModel
    {
        [FromForm(Name = "user-firstname")]
        public String Firstname { get; set; } = null!;

        [FromForm(Name = "user-lastname")]
        public String Lastname { get; set; } = null!;

        [FromForm(Name = "user-tel")]
        public String Tel { get; set; } = null!;

        [FromForm(Name = "user-mail")]
        public String Mail { get; set; } = null!;
    }
}
