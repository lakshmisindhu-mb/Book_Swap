﻿using Azure;
using Book_Swap_Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

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
            apiUrl = "http://localhost:81/api/Book";
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

        [HttpPost]
        public async Task<IActionResult> SearchBook(string searchString, bool notUsed)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bookList = client.PostAsJsonAsync(apiUrl + string.Format("/SearchBook"), searchString).Result;
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
            var editedbookDetail = client.PutAsJsonAsync(apiUrl+"/UpdateBook", bookList).Result;
            if (editedbookDetail.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int id = 0)
        {
            var bookDetailView = client.GetAsync(apiUrl + string.Format("/GetBookDetails?booklist=" + id)).Result;
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

        public async Task<IActionResult> SearchBook(IFormCollection form)
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
                    var bookList = client.GetAsync(apiUrl + string.Format("/SearchBook?searchText=" + searchText)).Result;
                    
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
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
