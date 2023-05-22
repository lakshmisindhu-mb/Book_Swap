using Book_Swap_Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Swap_UI_Design.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client;

        public UserController()
        {
            client = new HttpClient();
        }
        public IActionResult Index()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string apiUrl = "https://localhost:7177/api/Book";
                    var bookList = client.GetAsync(apiUrl + string.Format("/GetBookLIst")).Result;
                    if (bookList.IsSuccessStatusCode)
                    {
                        return View(bookList);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(BookList bookList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getEmployee = client.PostAsJsonAsync("api/AddBook", bookList).Result;
                    if (getEmployee.IsSuccessStatusCode)
                    {
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult Edit(int id = 0)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(BookList bookLIst)
        {
            var getEmployee = client.PostAsJsonAsync("api/AddBook", bookLIst).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return View();
            }
            return View(getEmployee);
        }
        public ActionResult Details(int id = 0)
        {
            var getEmployee = client.PostAsJsonAsync("api/AddBook", id).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return View();
            }
            return View(getEmployee);
        }
        public ActionResult Delete(int id = 0)
        {
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var getEmployee = client.PostAsJsonAsync("api/AddBook", id).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return View();
            }
            return View(getEmployee);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
