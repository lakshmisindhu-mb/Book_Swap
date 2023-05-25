using Azure;
using Book_Swap_Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Book_Swap_UI_Design.Controllers
{
    public class BookController : Controller
    {

        private readonly HttpClient client;
        private readonly string apiUrl;
        private List<BookList> bookList1;
        private BookList bookDetails;
        public BookController()
        {
            client = new HttpClient();
            apiUrl = "https://localhost:7177/api/Book";
            bookList1 = new List<BookList>();
            bookDetails = new BookList();
        }
        public async Task<IActionResult> Index()
        {
            try
            {                
                if (ModelState.IsValid)
                {                    
                    var bookList = client.GetAsync(apiUrl+ string.Format("/GetBookLIst")).Result;
                    if (bookList.IsSuccessStatusCode)
                    {
                        string apiResponse = await bookList.Content.ReadAsStringAsync();
                        bookList1 = JsonConvert.DeserializeObject<List<BookList>>(apiResponse);
                        return View(bookList1);
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
                    var getEmployee = client.PostAsJsonAsync(apiUrl+"api/AddBook", bookList).Result;
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
        public async Task<ActionResult> Edit(int id = 0)
        {
            var bookList = client.GetAsync(apiUrl + string.Format("/GetBookDetails?booklist="+id)).Result;
            if (bookList.IsSuccessStatusCode)
            {
                string apiResponse = await bookList.Content.ReadAsStringAsync();
                bookDetails = JsonConvert.DeserializeObject<BookList>(apiResponse);
                return View(bookDetails);
            }
            return View(bookDetails);
        }
        [HttpPost]
        public ActionResult Edit(BookList bookList)
        {
            var getEmployee = client.PutAsJsonAsync(apiUrl+"/UpdateBook", bookList).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id = 0)
        {
            var getEmployee = client.PostAsJsonAsync(apiUrl + "api/GetBookDetails", id).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return View();
            }
            return View(getEmployee);
        }
        public async Task<ActionResult> Delete(int id = 0)
        {
            var bookList = client.GetAsync(apiUrl + string.Format("/GetBookDetails?booklist=" + id)).Result; 
            if (bookList.IsSuccessStatusCode)
            {
                string apiResponse = await bookList.Content.ReadAsStringAsync();
                bookDetails = JsonConvert.DeserializeObject<BookList>(apiResponse);
                return View(bookDetails);
            }
            return View(bookDetails);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var getEmployee = client.PostAsJsonAsync(apiUrl + "/DeleteBook", id).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddUserBookTransaction()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUserBookTransaction(UserBookTransaction UserBookTransaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getEmployee = client.PostAsJsonAsync("/AddUserBookTransaction", UserBookTransaction).Result;
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
        public ActionResult UpdateUserBookTransaction(int id = 0)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateUserBookTransaction(UserBookTransaction UserBookTransaction)
        {
            var getEmployee = client.PostAsJsonAsync("/UpdateUserBookTransaction", UserBookTransaction).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return View();
            }
            return View(getEmployee);
        }
        public IActionResult GetUserBookTransaction()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bookList = client.GetAsync(apiUrl + string.Format("/GetUserBookTransaction")).Result;
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
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
