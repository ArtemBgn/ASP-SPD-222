using ASP_SPD_222.Data;
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
        private readonly DataContext _dataContext;

        // конструктор зазначає необхідні залежносі, їх передає - Resolver (Injector)
        public HomeController(ILogger<HomeController> logger, IHashService hashService, IValService valService, DataContext dataContext)
        {
            _logger = logger;
            _hashService = hashService;
            _valService = valService;
            _dataContext = dataContext;
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
                    if(_valService.ValNameString(formModelVal.UserFirstname) && _valService.ValNameString(formModelVal.UserLastname) && _valService.ValTelString(formModelVal.UserTel) && _valService.ValMailString(formModelVal.UserMail))
                    {
                        _dataContext.MyFormDataBases.Add(new()
                        {
                            Id = Guid.NewGuid(),
                            Firstname = formModelVal.UserFirstname,
                            Lastname = formModelVal.UserLastname,
                            Tel = formModelVal.UserTel,
                            Mail = formModelVal.UserMail,
                            MomentReg = DateTime.Now,
                        });
                        _dataContext.SaveChanges();
                    }
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
        public ViewResult SignUp()
        {
            SignupViewModel viewModel = new();

            // перевіряємо, чи є дані від форми
            if (HttpContext.Session.Keys.Contains("formStatus"))
            {
                // декодуємо статус
                viewModel.FormStatus = Convert.ToBoolean(
                    HttpContext.Session.GetString("formStatus"));
                HttpContext.Session.Remove("formStatus");

                // перевіряємо - якщо помилковий, то у сесії дані валідації і моделі
                if (viewModel.FormStatus ?? false)
                {
                    viewModel.FormModel = null;
                    viewModel.FormValidation = null;
                }
                else
                {
                    viewModel.FormModel = JsonSerializer
                        .Deserialize<SignupFormModel>(
                            HttpContext.Session.GetString("formModel")!);
                    HttpContext.Session.Remove("formModel");

                    viewModel.FormValidation = JsonSerializer
                        .Deserialize<SignupFormValidation>(
                            HttpContext.Session.GetString("formValidation")!);
                    HttpContext.Session.Remove("formValidation");
                }
            }
            return View(viewModel);
        }
        [HttpPost]
        public RedirectToActionResult SignupForm(SignupFormModel model)
        {
            SignupFormValidation results = new();
            bool isFormValid = true;
            if (String.IsNullOrEmpty(model.Login))
            {
                results.LoginErrorMessage = "Логін не може бути порожним";
                isFormValid = false;
            }
            if (String.IsNullOrEmpty(model.Name))
            {
                results.NameErrorMessage = "ПІБ не може бути порожним";
                isFormValid = false;
            }
            if (String.IsNullOrEmpty(model.Email))
            {
                results.EmailErrorMessage = "Email не може бути порожним";
                isFormValid = false;
            }
            if (String.IsNullOrEmpty(model.Password))
            {
                results.PasswordErrorMessage = "Пароль не може бути порожним";
                isFormValid = false;
            }
            if (model.Password != model.Repeat)
            {
                results.RepeatErrorMessage = "Повтор не збігається з паролем";
                isFormValid = false;
            }

            if (isFormValid && model.Avatar != null &&
                model.Avatar.Length > 0)  // поле не обов'язкове, але якщо є, то перевіряємо
            {
                // при збереженні (uploading) файлів слід міняти їх імена.
                int dotPosition = model.Avatar.FileName.LastIndexOf(".");
                if (dotPosition == -1)
                {
                    results.AvatarErrorMessage = "Файли без розширення не приймаються";
                    isFormValid = false;
                }
                else
                {
                    String ext = model.Avatar.FileName.Substring(dotPosition);
                    // TODO: додати перевірку розширення на перелік дозволених

                    // генеруємо випадкове ім'я файлу, зберігаємо розширення
                    // контролюємо, що такого імені немає у сховищі
                    String dir = Directory.GetCurrentDirectory();
                    String savedName;
                    String fileName;
                    do
                    {
                        fileName = Guid.NewGuid() + ext;
                        savedName = Path.Combine(dir, "wwwroot", "avatars", fileName);
                    }
                    while (System.IO.File.Exists(savedName));
                    using Stream stream = System.IO.File.OpenWrite(savedName);
                    model.Avatar.CopyTo(stream);

                    // до цього місця доходимо у разі відсутності помилок валідації
                    // додаємо нового користувача до БД
                    
                    String salt = _hashService.HexString(Guid.NewGuid().ToString());
                    String dk = _hashService.HexString(salt + model.Password);
                    _dataContext.Users.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        Login = model.Login,
                        Name = model.Name,
                        Avatar = fileName,
                        RegisterDt = DateTime.Now,
                        DeleteDt = null,
                        Email = model.Email,
                        PasswordSalt = salt,
                        PasswordDk = dk,
                    });
                    _dataContext.SaveChanges();//*/
                }
            }
                if (!isFormValid)
            {
                HttpContext.Session.SetString("formModel",
                    JsonSerializer.Serialize(model));

                HttpContext.Session.SetString("formValidation",
                    JsonSerializer.Serialize(results));
            }
            HttpContext.Session.SetString("formStatus",
                isFormValid.ToString());

            return RedirectToAction(nameof(SignUp));//*/
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}