using ASP_SPD_222.Data;
using ASP_SPD_222.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Security.Claims;

namespace ASP_SPD_222.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ViewResult Profile(String? id)
        {
            UserProfileViewModel model = new();
            if (id == null) //спроба доступу до свого кабінету
            {
                //перевіряємо автентифікацію
                if (HttpContext.User.Identity?.IsAuthenticated ?? false)
                {
                    model.IsPersonal = true;
                    // шукаємо дані користувача за Claim
                    String sid = HttpContext
                        .User
                        .Claims
                        .First(claim => claim.Type == ClaimTypes.Sid)
                        .Value;
                    // шукаємо у контексті даних (у БД)
                    model.User = _dataContext.Users.Find(Guid.Parse(sid));

                }
                else //спроба доступу без входу в систему
                {
                    model.IsPersonal = false;
                    model.User = null;
                }
            }
            else //вказано id - доступ до "чужого" профілю
            {
                model.IsPersonal = false;
                model.User = _dataContext
                    .Users
                    .FirstOrDefault(u => u.Login == id);
            }
            ViewData["id"] = id;
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateProfile(String newName, String newEmail)
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                // шукаємо дані користувача за Claim
                String sid = HttpContext
                    .User
                    .Claims
                    .First(claim => claim.Type == ClaimTypes.Sid)
                    .Value;
                // шукаємо у контексті даних (у БД)
                var user = _dataContext.Users.Find(Guid.Parse(sid));
                if (user != null)
                {
                    user.Name = newName;
                    user.Email = newEmail;
                    await _dataContext.SaveChangesAsync();
                    return Json(new { status = 200 });
                }
            }
            return Json(new { status = 401 });
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteProfile()
        {
            var user = this.GetAuthUser();
            if (user == null) { return Json(new { status = 401 }); }
            // _dataContext.Users.Remove(user); - повне видалення - порушення зв'язків данних
            user.DeleteDt = DateTime.Now; // встановлюємо "ознаку" видалення
            // за вимогами законодавства видаляємо персональні данні
            user.Name = "";
            user.Email = "";
            if(user.Avatar != null)
            {
                String dir = Directory.GetCurrentDirectory();
                String avatarFileName = Path.Combine(dir, "wwwroot", "avatars", user.Avatar);
                if (System.IO.File.Exists(avatarFileName))
                {
                    System.IO.File.Delete(avatarFileName);
                }
                user.Avatar = null;
            }
            user.Login = "";
            user.Avatar = "";
            user.PasswordDk = "";
            user.PasswordSalt = "";
            await _dataContext.SaveChangesAsync();//*/
            return Json(new { status = 200 });
        }
        [HttpGet]
        public async Task<JsonResult> OffProfile()
        {
            var user = this.GetAuthUser();
            if (user == null) { return Json(new { status = 401 }); }
            user.DeleteDt = DateTime.Now; // встановлюємо "ознаку" видалення
            // у зв'язку з виключенням профілю не видаляємо персональні данні
            await _dataContext.SaveChangesAsync();//*/
            return Json(new { status = 200 });
        }
        private Data.Entities.User? GetAuthUser()
        {
            //перевіряємо автентифікацію
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                // шукаємо дані користувача за Claim
                String sid = HttpContext
                    .User
                    .Claims
                    .First(claim => claim.Type == ClaimTypes.Sid)
                    .Value;
                // шукаємо у контексті даних (у БД)
                return _dataContext.Users.Find(Guid.Parse(sid));

            }
            return null;
        }
        [HttpGet]
        public async Task<JsonResult> RecoveryProfile()
        {
            UserProfileViewModel model = new();
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                model.IsPersonal = true;
                // шукаємо дані користувача за Claim
                String sid = HttpContext
                    .User
                    .Claims
                    .First(claim => claim.Type == ClaimTypes.Sid)
                    .Value;
                // шукаємо у контексті даних (у БД)
                model.User = _dataContext.Users.Find(Guid.Parse(sid));
                model.User.DeleteDt = null;
                await _dataContext.SaveChangesAsync();
                return Json(new { status = 200 });
            }
            return Json(new { status = 300 });
        }
    }
}
