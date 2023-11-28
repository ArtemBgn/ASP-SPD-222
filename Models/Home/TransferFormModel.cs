using Microsoft.AspNetCore.Mvc;

namespace ASP_SPD_222.Models.Home
{
    public class TransferFormModel
    {
        [FromForm(Name = "user-firstname")]
        public String UserFirstname { get; set; } = null!;

        [FromForm(Name = "user-lastname")]
        public String UserLastname { get; set; } = null!;
    }
}
