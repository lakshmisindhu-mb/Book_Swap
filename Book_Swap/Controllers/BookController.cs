using Book_Swap_DL;
using Book_Swap_Models;
using Book_Swap_Models.Models;
using Book_Swap_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Book_Swap_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookSwapContext bookSwapContext;
        private readonly IBookInterface bookInterface;
        private readonly CrudStatus crudStatus;
        private BookList bookDetail;
        private UserBookTransaction userbookTransactionDetail;

        public BookController(BookSwapContext bookSwapContext, IBookInterface bookInterface)
        {
            this.bookSwapContext = bookSwapContext;
            this.bookInterface = bookInterface;
            crudStatus = new CrudStatus();
            bookDetail = new BookList();
            userbookTransactionDetail = new UserBookTransaction();
        }

        [HttpGet]
        [Route("GetBookList")]

        public JsonResult GetBookList()
        {
            try
            {
                List<BookList> list = bookInterface.GetBookList();
                return new JsonResult(list);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("SearchBook")]

        public JsonResult SearchBook(string? searchText = "")
        {
            try
            {
                List<BookList> list = bookInterface.SearchBook(searchText);
                return new JsonResult(list);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddBook")]
        public JsonResult AddBook(BookList bookList)
        {
            try
            {
                bookInterface.AddBook(bookList);
                crudStatus.Message = "Book Added Succesfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateBook")]
        public JsonResult UpdateBook(BookList booklist)
        {
            try
            {
                bookInterface.UpdateBook(booklist);
                crudStatus.Message = "Book Updated Successfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public JsonResult DeleteBook(BookList booklist)
        {
            try
            {
                bookInterface.DeleteBook(booklist);
                crudStatus.Message = "Book Deleted Successfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUserBookTransaction")]
        public JsonResult DeleteUserBookTransaction(UserBookTransaction userBookTransaction)
        {
            try
            {
                bookInterface.DeleteUserBookTransaction(userBookTransaction);
                crudStatus.Message = "User Book Transaction Deleted Successfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBookDetails")]
        public BookList GetBookDetails(int booklist)
        {
            try
            {
                bookDetail = bookInterface.GetBookDetails(booklist);
                crudStatus.Message = "Book Details";
                
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return bookDetail;
        }

        [HttpGet]
        [Route("GetUserBookTransactionDetails")]
        public UserBookTransaction GetUserBookTransactionDetails(int booklist)
        {
            try
            {
                userbookTransactionDetail = bookInterface.GetUserBookTransactionDetails(booklist);
                crudStatus.Message = "User book Transaction Details";

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return userbookTransactionDetail;
        }

        [HttpPost]
        [Route("AddUserBookTransaction")]
        public JsonResult AddUserBookTransaction(UserBookTransaction transaction)
        {
            try
            {
                bookInterface.AddUserBookTransaction(transaction);
                crudStatus.Message = "User book Transaction Added Succesfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUserBookTransaction")]
        public JsonResult UpdateUserBookTransaction(UserBookTransaction transaction)
        {
            try
            {
                bookInterface.UpdateUserBookTransaction(transaction);
                crudStatus.Message = "User book Transaction Updated Successfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserBookTransaction")]
        public List<GetUserBookTransaction> GetUserBookTransaction(int borrowerId, int lenderId)
        {
            List<GetUserBookTransaction> transactions = new();
            try
            {
                transactions = bookInterface.GetUserBookTransaction(borrowerId, lenderId);
                crudStatus.Message = "User book Transactions fetched Successfully";
                return transactions;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return transactions;
            }
        }
    }
}
