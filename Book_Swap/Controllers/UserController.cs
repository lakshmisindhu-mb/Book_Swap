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

        public UserController(BookSwapContext bookSwapContext, IUserInterface userInterface)
        {
            this.bookSwapContext = bookSwapContext;
            this.userInterface = userInterface;
            crudStatus = new CrudStatus();
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

        [HttpPut]
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
        public JsonResult SearchUser(int Id)
        {
            try
            {
                userInterface.SearchUser(Id);
                crudStatus.Message = "User Searched Successfully";
                return new JsonResult(crudStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("RateUser")]
        public JsonResult RateUser(RateUserRequest request)
        {
            try
            {
                var response = userInterface.RateUser(request);
               
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

    }
}
