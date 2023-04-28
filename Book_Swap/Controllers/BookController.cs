using Book_Swap_DL;
using Book_Swap_Models;
using Book_Swap_Models.Models;
using Book_Swap_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Swap_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookSwapContext bookSwapContext;
        private readonly IBookInterface bookInterface;
        private readonly CrudStatus crudStatus;

        public BookController(BookSwapContext bookSwapContext, IBookInterface bookInterface)
        {
            this.bookSwapContext = bookSwapContext;
            this.bookInterface = bookInterface;
            crudStatus = new CrudStatus();
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
