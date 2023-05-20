using Microsoft.AspNetCore.Mvc;

namespace Book_Swap_UI_Design.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
