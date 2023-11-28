using Microsoft.AspNetCore.Mvc;

namespace ASP_SPD_222.Models.Home
{
    public class HomeWorkAspTwoFormModel
    {
        [FromForm(Name = "user-firstname")]
        public String UserFirstname { get; set; } = null!;

        [FromForm(Name = "user-lastname")]
        public String UserLastname { get; set; } = null!;

        [FromForm(Name = "user-tel")]
        public String UserTel { get; set; } = null!;

        [FromForm(Name = "user-mail")]
        public String UserMail { get; set; } = null!;
    }
}
