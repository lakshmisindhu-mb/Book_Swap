using Book_Swap_DL;
using Book_Swap_Models;
using Book_Swap_Models.Models;
using Book_Swap_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Book_Swap_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : Controller
    {
        private readonly BookSwapContext _bookSwapContext;
        private readonly IWishListService _wishListService;

        public WishListController(BookSwapContext bookSwapContext, IWishListService wishListService)
        {
            _bookSwapContext = bookSwapContext;
            _wishListService = wishListService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishList([FromBody] WishListBookModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //TODO: Should we check if the book is already in the wish list?
                    //TODO: Should we add authorization. 
                    var result = await _wishListService.AddWishList(model);
                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Book could not be added to the wish list");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWishListedBooks()
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //TODO: Should we check if the book is already in the wish list?
                    //TODO: Should we add authorization. 
                    var result = await _wishListService.GetWishListBooks();
                    if (result.Count > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return Ok("No books are present in the wish list yet!!!");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("{userName}")]
        public async Task<IActionResult> GetUserWishListedBooks(string userName)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //TODO: Should we check if the book is already in the wish list?
                    //TODO: Should we add authorization. 
                    var result = await _wishListService.GetWishListBooks(userName);
                    if (result.Count > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return Ok("No books are present in your wish list");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookFromWishList([FromBody] DeleteWishListBookRequestModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //TODO: Should we check if the book is already in the wish list?
                    //TODO: Should we add authorization.

                    var bookList = await _wishListService.GetWishListBooks(request.UserName);
                    bool isBookExist = bookList.Where(_ => _.BookName == request.BookName).Any();

                    if (isBookExist)
                    {

                        var result = await _wishListService.DeleteBookFromWishList(request);
                        if (result > 0)
                        {
                            return Ok("Book deleted from your wishlist");
                        }
                        else
                        { 
                            return BadRequest("Book could not be deleted"); 
                        }
                    }
                    else
                    {
                        return Ok($"Book {request.BookName} does not exist in your wish list");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
