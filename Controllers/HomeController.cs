using ASP_SPD_222.Models;
using ASP_SPD_222.Models.Home;
using ASP_SPD_222.Services.Hash;
using ASP_SPD_222.Services.Val;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace ASP_SPD_222.Controllers
{
    public class HomeController : Controller
    {
        //залежність від служби заявляється як private readonly none
        private readonly IHashService _hashService; //DIP - тип залежності - це інтерфейс
        private readonly IValService _valService;
        private readonly ILogger<HomeController> _logger;

        // конструктор зазначає необхідні залежносі, їх передає - Resolver (Injector)
        public HomeController(ILogger<HomeController> logger, IHashService hashService, IValService valService)
        {
            _logger = logger;
            _hashService = hashService;
            _valService = valService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
        public ViewResult Ioc()
        {
            //використовуємо сервіс
            ViewData["hash"] = _hashService.HexString("123");
            ViewData["objHash"] = _hashService.GetHashCode();
            //ViewData["valid"] = _valService.ValString("123");
            return View();
        }
        public ViewResult Transfer()
        {
            TransferFormModel? formModel;

            if (HttpContext.Session.Keys.Contains("formModel"))
            {
                // Якщо є у сессії дані - відновлюємо їх та видаляємо з сессії
                formModel = JsonSerializer.Deserialize<TransferFormModel>(HttpContext.Session.GetString("formModel")!);
                HttpContext.Session.Remove("formModel");
            }
            else { formModel = null; }
            TransferViewModel model = new()
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                ControllerName = this.GetType().Name,
                formModel = formModel
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult ProcessTransferForm(TransferFormModel? formModel)
        {
            // Зберігаємо у сессії серіалізваний об'єкт formModel під
            // іменем "formModel"
            HttpContext.Session.SetString("formModel", JsonSerializer.Serialize(formModel));

            return RedirectToAction(nameof(Transfer));
        }
        [HttpPost]
        public IActionResult ProcessValForm(HomeWorkAspTwoFormModel? formModelVal)
        {
            if(formModelVal != null)
            {
                if( (formModelVal.UserFirstname != null) || (formModelVal.UserLastname != null) || (formModelVal.UserTel != null) || (formModelVal.UserMail != null) )
                {
                    HttpContext.Session.SetString("FormModelVal", JsonSerializer.Serialize(formModelVal));
                }
            }
            
            return RedirectToAction(nameof(HomeWorkAspTwo));
        }

        public ViewResult HomeWorkAspTwo()
        {
            HomeWorkAspTwoFormModel? formModelVal;
            if (HttpContext.Session.Keys.Contains("FormModelVal"))
            {
                formModelVal = JsonSerializer.Deserialize<HomeWorkAspTwoFormModel>(HttpContext.Session.GetString("FormModelVal")!);
                HttpContext.Session.Remove("FormModelVal");
            }
            else { formModelVal = null; }
            HomeWorkAspTwoViewModel model = new()
            {
                //ControllerName = this.GetType().Name,
                FormModelVal = formModelVal
            };
            return View(model);
        }
        public ViewResult Db()
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