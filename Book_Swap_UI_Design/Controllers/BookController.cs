using Book_Swap_Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Book_Swap_UI_Design.Controllers
{
    public class BookController : Controller
    {

        private readonly HttpClient client;

        public BookController()
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
                    var bookList = client.GetAsync(apiUrl+ string.Format("/GetBookLIst")).Result;
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
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(BookList bookList)
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
                return View();
            }
        }

        //

        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
           
             return View(); 
        }

        //
        // POST: /Employee/Edit/5
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

        //

        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            var getEmployee = client.PostAsJsonAsync("api/AddBook", id).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return View();
            }
            return View(getEmployee);
        }

        //
        // GET: /Employee/Delete/5
        public ActionResult Delete(int id = 0)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5
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
