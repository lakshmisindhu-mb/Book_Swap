using Book_Swap_DL;
using Book_Swap_Models;
using Book_Swap_Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Service
{
    public class UserService:IUserInterface
    {
        private readonly BookSwapContext bookSwapContext;
        public UserService(BookSwapContext bookSwapContext)
        {
            this.bookSwapContext = bookSwapContext;
        }
     
        public void UpdateUser(User userList)
        {
            try
            {
                User user = bookSwapContext.Users.Where(y => y.Id == userList.Id).FirstOrDefault()!;
                user.UpdatedDate = userList.UpdatedDate;
                user.UserKey = userList.UserKey;
                bookSwapContext.Entry(user).State = EntityState.Modified;
                bookSwapContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }

        public void DeleteUser(int Id)
        {
            try
            {               
                User user = bookSwapContext.Users.Where(y => y.Id == Id).FirstOrDefault()!;
                bookSwapContext.Entry(user).State = EntityState.Deleted;
                bookSwapContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void SearchUser(int Id)
        {
            //BookList book = bookSwapContext.BookLists.Where(y => y.Id == bookList.Id).FirstOrDefault()!; 
            //book.BookName = bookList.BookName;
            //book.GenreId = bookList.GenreId;    
            //book.Author = bookList.Author;
            //book.Publisher = bookList.Publisher;
            //book.Edition = bookList.Edition;
            //book.ReleaseDate = bookList.ReleaseDate;
            //book.ModifiedDate = DateTime.Now;
            //bookSwapContext.Entry(book).State = EntityState.Modified;
            //bookSwapContext.SaveChanges();
        }



    }
}
