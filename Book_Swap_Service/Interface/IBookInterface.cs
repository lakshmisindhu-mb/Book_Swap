using Book_Swap_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Interface
{
    public interface IBookInterface
    {
        public void AddBook(BookList bookList);
        public void UpdateBook(BookList bookList);
    }
}
