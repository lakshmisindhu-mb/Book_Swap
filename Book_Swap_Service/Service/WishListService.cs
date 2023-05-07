using Book_Swap_DL;
using Book_Swap_Models;
using Book_Swap_Models.Models;
using Book_Swap_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Service
{
    public class WishListService : IWishListService
    {
        private readonly BookSwapContext bookSwapContext;
        public WishListService(BookSwapContext bookSwapContext)
        {
            this.bookSwapContext = bookSwapContext;
        }

        public async Task<int> AddWishList(WishListBookModel model)
        {
            int result = 0;
            try
            {
                WishListBook wishList = new WishListBook();
                wishList.BookName = model.BookName;
                wishList.UserName = model.UserName;
                wishList.EmailId = model.EmailId;
                wishList.Phone = model.Phone;
                wishList.Author = model.Author;
                wishList.Publisher = model.Publisher;
                wishList.Edition = model.Edition;
                wishList.CreatedDate = DateTime.Now.ToUniversalTime();
                wishList.ModifiedDate = DateTime.Now.ToUniversalTime();
                wishList.DeadLineDate = DateTime.Now.ToUniversalTime().AddMonths(2);
                wishList.WishlistedDate = DateTime.Now.ToUniversalTime();

                bookSwapContext.WishListBooks.Add(wishList);
                result = await bookSwapContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<WishListBook>> GetWishListBooks()
        {
            return await bookSwapContext.WishListBooks.ToListAsync();
        }

        public async Task<List<WishListBook>> GetWishListBooks(string UserName)
        {
            return await bookSwapContext.WishListBooks.Where(_ => _.UserName == UserName!).ToListAsync();
        }

        public async Task<int> DeleteBookFromWishList(DeleteWishListBookRequestModel model)
        {
            int result = 0;
            try
            {
                var book = bookSwapContext.WishListBooks.Where(_ => _.UserName == model.UserName && _.BookName == model.BookName).FirstOrDefault();
                if (book != null)
                {
                    result = await bookSwapContext.SaveChangesAsync();
                    bookSwapContext.Entry(book).State = EntityState.Deleted;
                    result = bookSwapContext.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
