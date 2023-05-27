using Book_Swap_Models;
using Book_Swap_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Interface
{
    public interface IBookInterface
    {
        public List<BookList> GetBookList();
        List<BookList> SearchBook(string? searchText);
        public void AddBook(BookList bookList);
        public void UpdateBook(BookList bookList);
        public void DeleteBook(BookList bookID);
        public BookList GetBookDetails(int bookID);
        public void AddUserBookTransaction(UserBookTransaction transaction);
        public void UpdateUserBookTransaction(UserBookTransaction transaction);
        public List<GetUserBookTransaction> GetUserBookTransaction(int borrowerId, int lenderId);
        public void DeleteUserBookTransaction(UserBookTransaction userBookTransactionID);
        public UserBookTransaction GetUserBookTransactionDetails(int userBookTransactionID);
        public void RequestBook(RequestBook request);
    }
}
