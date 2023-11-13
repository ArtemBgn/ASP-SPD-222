using ASP_SPD_222.Models;
using ASP_SPD_222.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_SPD_222.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Transfer()
        {
            TransferViewModel model = new()
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                ControllerName = this.GetType().Name,
            };
            return View(model);
        }
        public IActionResult AllHomeWorkAspOne()
        {
            return View();
        }
        public IActionResult HomeWorkAspOne()
        {
            HomeWorkAspOneViewModel model1 = new()
            {
                Day = DateTime.Now.DayOfYear,
            };
            return View(model1);
        }
        public IActionResult HomeWorkAspTaskTwo()
        {
            String str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            int r = random.Next(0, 25);

            HomeWorkAspTaskTwoViewModel model2 = new()
            {
                EnglishLetter = str.ToCharArray()[r],
            };
            return View(model2);
        }
        public IActionResult Razor()
        {
            ViewData["formController"] = "Hello from Controller";
            return View();
        }        
         public IActionResult BruklinLend()
        {
            return View();
        }
        public IActionResult HomeWorkAspTaskFour()
        {
            string[] str = {"Добремісто", "Бруклин", "Браво-Пицца", "Майфемели"};
            Random random = new Random();
            int r = random.Next(0, 3);

            HomeWorkAspTaskFourViewModel model4 = new()
            {
                NameTavern = str[r],
            };
            return View(model4);
        }
        public IActionResult Cuntry()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}