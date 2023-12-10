using ASP_SPD_222.Data;
using ASP_SPD_222.Services.Hash;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_SPD_222.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IHashService _hashService;
        public AuthController(DataContext dataContext, IHashService hashService)
        {
            _dataContext = dataContext;
            _hashService = hashService;
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
    }
}
/**/