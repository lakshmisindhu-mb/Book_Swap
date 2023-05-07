using Book_Swap_Models;
using Book_Swap_Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Interface
{
    public interface IWishListService
    {
        Task<int> AddWishList(WishListBookModel model);
        Task<List<WishListBook>> GetWishListBooks();
        Task<List<WishListBook>> GetWishListBooks(string UserName);
        Task<int> DeleteBookFromWishList(DeleteWishListBookRequestModel model);
    }
}
