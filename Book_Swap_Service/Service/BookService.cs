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
    public class BookService:IBookInterface
    {
        private readonly BookSwapContext bookSwapContext;
        public BookService (BookSwapContext bookSwapContext)
        {
            this.bookSwapContext = bookSwapContext;
        }
        public void AddBook(BookList bookList)
        {
            bookList.CreatedDate = DateTime.Now;
            bookList.ModifiedDate = null;
            bookSwapContext.BookLists.Add(bookList);
            bookSwapContext.SaveChanges();
        }
        public void UpdateBook(BookList bookList)
        {
            BookList book = bookSwapContext.BookLists.Where(y => y.Id == bookList.Id).FirstOrDefault()!; 
            book.BookName = bookList.BookName;
            book.GenreId = bookList.GenreId;    
            book.Author = bookList.Author;
            book.Publisher = bookList.Publisher;
            book.Edition = bookList.Edition;
            book.ReleaseDate = bookList.ReleaseDate;
            book.ModifiedDate = DateTime.Now;
            bookSwapContext.Entry(book).State = EntityState.Modified;
            bookSwapContext.SaveChanges();
        }


    }
}
