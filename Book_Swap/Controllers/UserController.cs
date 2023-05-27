using Book_Swap_DL;
using Book_Swap_Models;
using Book_Swap_Models.Models;
using Book_Swap_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Swap_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BookSwapContext bookSwapContext;
        private readonly IUserInterface userInterface;
        private readonly CrudStatus crudStatus;
        private User userDetail;

        public UserController(BookSwapContext bookSwapContext, IUserInterface userInterface)
        {
            this.bookSwapContext = bookSwapContext;
            this.userInterface = userInterface;
            crudStatus = new CrudStatus();
            userDetail = new User();
        }

        [HttpGet]
        [Route("UserList")]

        public JsonResult GetUserList()
        {
            try
            {
                List<User> list = userInterface.GetUserList();
                return new JsonResult(list);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public JsonResult Register(User reg)
        {
            try
            {
                bool result = userInterface.CheckEmail(reg);
                if (result == false)
                {
                    string message = userInterface.Register(reg);
                    crudStatus.Message = message;
                }
                else
                {
                    crudStatus.Message = "Email Already Exists";
                }
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        [HttpPost]
        [Route("Login")]
        public JsonResult Login(User login)
        {
            try
            {
                string message = userInterface.Login(login);
                crudStatus.Message = message;
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public JsonResult Forgot_password(User forgetPwd)
        {
            try
            {
                string message=userInterface.ForgotPassword(forgetPwd);
                crudStatus.Message=message;
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public JsonResult UpdateUser(User userList)
        {
            try
            {
                userInterface.UpdateUser(userList);
                crudStatus.Message = "User Updated Successfully";
                return new JsonResult(crudStatus);
            }
            catch(Exception ex)
            {
                crudStatus.Message = "User Update Unsuccessfull due to some issue";
                return new JsonResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public JsonResult DeleteUser(int Id)
        {
            try
            {
                userInterface.DeleteUser(Id);
                crudStatus.Message = "User Deleted Successfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                crudStatus.Message = "User Deletion Unsuccessful due to some issue";
                return new JsonResult(ex.Message);
            }
        }


        [HttpGet]
        [Route("SearchUser")]
        public User SearchUser(string  username, string emailid)
        {
            User userDetails = new();
            try
            {
                userDetails = userInterface.SearchUser(username, emailid);
                crudStatus.Message = "User Searched Successfully";
                return userDetails;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return userDetails;             
            }
        }

        [HttpPost]
        [Route("RateUser")]
        public JsonResult RateUser(RateUserRequest request)
        {
            try
            {
                var response = userInterface.RateUser(request);
                crudStatus.Message = "User Rated Successfully";
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public User GetUserDetails(int user)
        {
            try
            {
                userDetail = userInterface.GetUserDetails(user);
                crudStatus.Message = "User Details fetched Successfully";

            }
            catch (Exception ex)
            {

            }
            return userDetail;
        }

    }
}
