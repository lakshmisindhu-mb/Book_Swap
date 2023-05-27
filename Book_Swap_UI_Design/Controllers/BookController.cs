using Azure;
using Book_Swap_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace Book_Swap_UI_Design.Controllers
{
    public class BookController : Controller
    {

        private readonly HttpClient client;
        private readonly string bookapiUrl;
        private List<BookList> bookList1;
        private BookList bookDetails;
        private List<User> userList1;
        private readonly string userapiUrl;
        private UserBookTransaction userBookTransactionDetails;
        public BookController()
        {
            client = new HttpClient();
            bookapiUrl = "http://localhost:81/api/Book";
            bookList1 = new List<BookList>();
            bookDetails = new BookList();
            userList1 = new List<User>();
            userapiUrl = "http://localhost:81/api/User";
            userBookTransactionDetails = new UserBookTransaction();
        }

        public async Task<IActionResult> Index()
        {
            try
            {                
                if (ModelState.IsValid)
                {                    
                    var bookList = client.GetAsync(bookapiUrl + string.Format("/GetBookLIst")).Result;
                    if (bookList.IsSuccessStatusCode)
                    {
                        string apiResponse = await bookList.Content.ReadAsStringAsync();
                        bookList1 = JsonConvert.DeserializeObject<List<BookList>>(apiResponse)!;
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

        [HttpPost]
        public async Task<IActionResult> SearchBook(string searchString, bool notUsed)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bookList = client.PostAsJsonAsync(bookapiUrl + string.Format("/SearchBook"), searchString).Result;
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
        public IActionResult AddNewBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewBook(BookList bookList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getEmployee = client.PostAsJsonAsync(bookapiUrl + "api/AddBook", bookList).Result;
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
            var bookList = client.GetAsync(bookapiUrl + string.Format("/GetBookDetails?booklist="+id)).Result;
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
            var editedbookDetail = client.PutAsJsonAsync(bookapiUrl + "/UpdateBook", bookList).Result;
            if (editedbookDetail.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int id = 0)
        {
            var bookDetailView = client.GetAsync(bookapiUrl + string.Format("/GetBookDetails?booklist=" + id)).Result;
            if (bookDetailView.IsSuccessStatusCode)
            {
                string apiResponse = await bookDetailView.Content.ReadAsStringAsync();
                bookDetails = JsonConvert.DeserializeObject<BookList>(apiResponse);
                return View(bookDetails);
            }
            return View(bookDetails);
        }
        public async Task<ActionResult> Delete(int id = 0)
        {
            var bookList = client.GetAsync(bookapiUrl + string.Format("/GetBookDetails?booklist=" + id)).Result; 
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
            var getEmployee = client.PostAsJsonAsync(bookapiUrl + "/DeleteBook", id).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AddUserBookTransactions()
        {
            var bookList = client.GetAsync(bookapiUrl + string.Format("/GetBookLIst")).Result;
            if (bookList.IsSuccessStatusCode)
            {
                string apiResponse = await bookList.Content.ReadAsStringAsync();
                bookList1 = JsonConvert.DeserializeObject<List<BookList>>(apiResponse)!;
                ViewBag.BookList = new SelectList(bookList1, "Id", "BookName");
            }

            var userlist = client.GetAsync(userapiUrl + string.Format("/UserList")).Result;
            if (userlist.IsSuccessStatusCode)
            {
                string apiResponse = await userlist.Content.ReadAsStringAsync();
                userList1 = JsonConvert.DeserializeObject<List<User>>(apiResponse)!;
                ViewBag.UserList = new SelectList(userList1, "Id", "UserName");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddUserBookTransactions(UserBookTransaction UserBookTransaction)
        {

            string borrowerID = Convert.ToString(Request.Form["UserDropList"]);
            string bookID = Convert.ToString(Request.Form["BookDropList"]);

            UserBookTransaction.BorrowerId = Convert.ToInt32(borrowerID);
            UserBookTransaction.BookId = Convert.ToInt32(bookID);
            try
            {
                if (ModelState.IsValid)
                {
                    var getEmployee = client.PostAsJsonAsync(bookapiUrl +string.Format("/AddUserBookTransaction"), UserBookTransaction).Result;
                    if (getEmployee.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetUserBookTransaction");
                    }
                }
                return RedirectToAction("GetUserBookTransaction");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public async Task<ActionResult> UpdateUserBookTransactionDetails(int id = 0)
        {

            var bookList = client.GetAsync(bookapiUrl + string.Format("/GetBookLIst")).Result;
            if (bookList.IsSuccessStatusCode)
            {
                string apiResponse = await bookList.Content.ReadAsStringAsync();
                bookList1 = JsonConvert.DeserializeObject<List<BookList>>(apiResponse)!;
                ViewBag.BookList = new SelectList(bookList1, "Id", "BookName");
            }

            var userlist = client.GetAsync(userapiUrl + string.Format("/UserList")).Result;
            if (userlist.IsSuccessStatusCode)
            {
                string apiResponse = await userlist.Content.ReadAsStringAsync();
                userList1 = JsonConvert.DeserializeObject<List<User>>(apiResponse)!;
                ViewBag.UserList = new SelectList(userList1, "Id", "UserName");
            }
            var bookDetailView = client.GetAsync(bookapiUrl + string.Format("/GetUserBookTransactionDetails?booklist=" + id)).Result;
            if (bookDetailView.IsSuccessStatusCode)
            {
                string apiResponse = await bookDetailView.Content.ReadAsStringAsync();
                userBookTransactionDetails = JsonConvert.DeserializeObject<UserBookTransaction>(apiResponse);
                return View(userBookTransactionDetails);
            }
            return View(userBookTransactionDetails);
        }
        [HttpPost]
        public ActionResult UpdateUserBookTransactionDetails(UserBookTransaction UserBookTransaction)
        {
            string borrowerID = Convert.ToString(Request.Form["UserDropList"]);
            string bookID = Convert.ToString(Request.Form["BookDropList"]);

            UserBookTransaction.BorrowerId = Convert.ToInt32(borrowerID);
            UserBookTransaction.BookId = Convert.ToInt32(bookID);

            var userbooktransaction = client.PostAsJsonAsync(bookapiUrl + string.Format("/UpdateUserBookTransaction"), UserBookTransaction).Result;
            if (userbooktransaction.IsSuccessStatusCode)
            {
                return View("Index");
            }
            return View("Index");
        }
        public async Task<IActionResult> GetUserBookTransactions()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bookList = client.GetAsync(bookapiUrl + string.Format("/GetUserBookTransaction")).Result;
                    if (bookList.IsSuccessStatusCode)
                    {
                        string apiResponse = await bookList.Content.ReadAsStringAsync();
                        List<GetUserBookTransaction> userBookTransactions = JsonConvert.DeserializeObject<List<GetUserBookTransaction>>(apiResponse)!;
                        return View(userBookTransactions);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> SearchBooks(IFormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string searchText = form["searchText"];
                    if (string.IsNullOrEmpty(searchText))
                    {
                        searchText = "";
                    }
                    var bookList = client.GetAsync(bookapiUrl + string.Format("/SearchBook?searchText=" + searchText)).Result;
                    
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

        public async Task<ActionResult> DetailsUserBookTransactionDetails(int id = 0)
        {
            var bookDetailView = client.GetAsync(bookapiUrl + string.Format("/GetUserBookTransactionDetails?booklist=" + id)).Result;
            if (bookDetailView.IsSuccessStatusCode)
            {
                string apiResponse = await bookDetailView.Content.ReadAsStringAsync();
                userBookTransactionDetails = JsonConvert.DeserializeObject<UserBookTransaction>(apiResponse);
                return View(userBookTransactionDetails);
            }
            return View(userBookTransactionDetails);
        }

        public async Task<ActionResult> DeleteUserBookTransactionDetails(int id = 0)
        {
            var bookList = client.GetAsync(bookapiUrl + string.Format("/GetUserBookTransactionDetails?booklist=" + id)).Result;
            if (bookList.IsSuccessStatusCode)
            {
                string apiResponse = await bookList.Content.ReadAsStringAsync();
                userBookTransactionDetails = JsonConvert.DeserializeObject<UserBookTransaction>(apiResponse);
                return View(userBookTransactionDetails);
            }
            return View(userBookTransactionDetails);
        }
        [HttpPost, ActionName("DeleteUserBookTransactionDetails")]
        public ActionResult DeleteUserBookTransactionConfirmed(int id)
        {
            var userBookTransaction = client.PostAsJsonAsync(bookapiUrl + "/DeleteUserBookTransaction", id).Result;
            if (userBookTransaction.IsSuccessStatusCode)
            {
                return RedirectToAction("GetUserBookTransaction");
            }
            return RedirectToAction("GetUserBookTransaction");
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
