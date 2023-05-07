using Book_Swap_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Interface
{
    public interface IUserInterface
    {
        public void UpdateUser(User userList);
        public void DeleteUser(int Id);
        public void SearchUser(int Id);

    }
}
