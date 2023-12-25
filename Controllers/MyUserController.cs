using ASP_SPD_222.Data;
using ASP_SPD_222.Models.MyUser;
using Microsoft.AspNetCore.Mvc;

namespace ASP_SPD_222.Controllers
{
    public class MyUserController : Controller
    {
        private readonly DataContext _dataContext;

        public MyUserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ViewResult MyProfile(String? id) //заповнити метод
        {
            MyUserProfileViewModel model = new();
            return View(model);
        }
    }
}
/*
 * Додати методи:
    UpdateProfile
    DeleteProfile
    GetAuthUser
 */