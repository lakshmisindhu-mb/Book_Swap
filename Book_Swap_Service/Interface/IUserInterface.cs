using Book_Swap_Models;
using Book_Swap_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Interface
{
    public interface IUserInterface
    {
        public List<User> GetUserList();
        bool CheckEmail(User emailCheck); 
        string Register(User register);
        string Login(User login);
        string ForgotPassword(User forgetPwd);
        public void UpdateUser(User userList);
        public void DeleteUser(int Id);
        public User SearchUser(string username, string emailid);
        RateUserResponse RateUser(RateUserRequest request);

        public User GetUserDetails(int bookID);

    }
}
