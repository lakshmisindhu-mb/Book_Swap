﻿using Book_Swap_DL;
using Book_Swap_Models;
using Book_Swap_Models.Models;
using Book_Swap_Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Service
{
    public class BookService : IBookInterface
    {
        private readonly BookSwapContext bookSwapContext;
        public BookService(BookSwapContext bookSwapContext)
        {
            this.bookSwapContext = bookSwapContext;
        }

        public List<BookList> GetBookList()
        {
            return bookSwapContext.BookLists.ToList();
        }
        public List<BookList> SearchBook(string? searchText)
        {
            if(string.IsNullOrEmpty(searchText))
            {
                return bookSwapContext.BookLists.ToList();
            }
            else
            {
                return bookSwapContext.BookLists.Where(x => x.BookName.Contains(searchText)).ToList();
            }
            
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

        public void DeleteBook(BookList bookID)
        {
            bookSwapContext.BookLists.Remove(bookID);
        }

       

        public BookList GetBookDetails(int bookID)
        {
            return bookSwapContext.BookLists.Find(bookID);
        }

        public UserBookTransaction GetUserBookTransactionDetails(int userBookTransactionID)
        {
            return bookSwapContext.UserBookTransactions.Find(userBookTransactionID);
        }

        public void DeleteUserBookTransaction(UserBookTransaction userBookTransactionID)
        {
            bookSwapContext.UserBookTransactions.Remove(userBookTransactionID);
        }

        public void AddUserBookTransaction(UserBookTransaction transaction)
        {
            transaction.BorrowDate = DateTime.Now;
            transaction.ReturnDate = null;
            transaction.Review = "";
            bookSwapContext.UserBookTransactions.Add(transaction);
            bookSwapContext.SaveChanges();
        }

        public void UpdateUserBookTransaction(UserBookTransaction transaction)
        {
            UserBookTransaction trans = bookSwapContext.UserBookTransactions.Where(x => x.Id == transaction.Id).FirstOrDefault()!;

            trans.Review = transaction.Review;

            if (transaction.ReturnDate != null && transaction.ReturnDate >= trans.BorrowDate)
            {
                trans.ReturnDate = transaction.ReturnDate;
            }
            else
            {
                trans.ReturnDate = DateTime.Now;
            }

            bookSwapContext.Entry(trans).State = EntityState.Modified;
            bookSwapContext.SaveChanges();
        }

        public List<GetUserBookTransaction> GetUserBookTransaction(int borrowerId, int lenderId)
        {
            List<GetUserBookTransaction> transactions = new();
            try
            {
                if (borrowerId == 0 && lenderId > 0)
                {
                    transactions = bookSwapContext.GetUserBookTransactions.Where(y => y.LenderId == lenderId).ToList();
                }
                else if (borrowerId > 0 && lenderId == 0)
                {
                    transactions = bookSwapContext.GetUserBookTransactions.Where(y => y.BorrowerId == borrowerId).ToList();
                }
                else if (borrowerId > 0 && lenderId > 0)
                {
                    transactions = bookSwapContext.GetUserBookTransactions.Where(y => y.BorrowerId == borrowerId && y.LenderId == lenderId).ToList();
                }
                else
                {
                    transactions = bookSwapContext.GetUserBookTransactions.ToList();
                }

                return transactions;
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public void RequestBook(RequestBook request)
        {
            if (request.BookId > 0 && !string.IsNullOrEmpty(request.BorrowerUsername) )
            {
                var book = bookSwapContext.BookLists.Where(x => x.Id == request.BookId).FirstOrDefault();
                var user = bookSwapContext.Users.Where(x => x.UserName == request.BorrowerUsername).FirstOrDefault();
                BookRequest brequest = new BookRequest { BorrowerId = user?.Id, BookId = book?.Id, OwnerId = book?.OwnerId };
                bookSwapContext.BookRequests.Add(brequest);
                bookSwapContext.SaveChanges();
            }
        }

    }
}
