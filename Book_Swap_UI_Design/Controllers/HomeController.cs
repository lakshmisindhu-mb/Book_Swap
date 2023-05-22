using Book_Swap_Models.Models;
using Book_Swap_UI_Design.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Swap_UI_Design.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = "https://localhost:7177/api/Book";
                var user = client.PostAsJsonAsync(apiUrl + string.Format("/Login"), login).Result;
                if (user.IsSuccessStatusCode)
                {                    
                    return Redirect("/home/welcomepage");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(login);
        }
    }
}