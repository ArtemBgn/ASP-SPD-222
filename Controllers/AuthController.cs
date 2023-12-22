using ASP_SPD_222.Data;
using ASP_SPD_222.Data.Entities;
using ASP_SPD_222.Models.Home;
using ASP_SPD_222.Services.Hash;
using ASP_SPD_222.Services.Val;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_SPD_222.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IHashService _hashService;
        private readonly IValService _valService;
        public AuthController(DataContext dataContext, IHashService hashService, IValService valService)
        {
            _dataContext = dataContext;
            _hashService = hashService;
            _valService = valService;
        }

        [HttpGet]
        public object Authenticate(String login, String password)
        {
            var user = _dataContext
                .Users
                .FirstOrDefault(u => u.Login == login);

            if (user == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new { status = "Credentials rejected" };
            }
            String dk = _hashService.HexString(user.PasswordSalt + password);
            if (user.PasswordDk != dk)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new { status = "Credentials rejected" };
            }
            HttpContext.Session.SetString("AuthUserId", user.Id.ToString());
            return new { status = "Ok", login, password };
        }

        [HttpDelete]
        public void SignOut()    // що передавать у метод?
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                HttpContext.Session.Remove("AuthUserId");
            }
        }
    }
}