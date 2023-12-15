using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ASP_SPD_222.Models.Home
{
    public class HomeWorkAspFourFormModel
    {
        [FromForm(Name = "hw-login")]
        public String Login { get; set; } = null!;

        [FromForm(Name = "hw-last-name")]
        public String LastName { get; set; } = null!;

        [FromForm(Name = "hw-first-name")]
        public String FirstName { get; set; } = null!;

        [FromForm(Name = "hw-father-name")]
        public String FatherName { get; set; } = null!;

        [FromForm(Name = "hw-email")]
        public String Email { get; set; } = null!;

        [FromForm(Name = "hw-password")]
        public String Password { get; set; } = null!;

        [FromForm(Name = "hw-repeat")]
        public String Repeat { get; set; } = null!;

        [FromForm(Name = "hw-avatar")]
        [JsonIgnore]
        public IFormFile Avatar { get; set; } = null!;
    }
}
