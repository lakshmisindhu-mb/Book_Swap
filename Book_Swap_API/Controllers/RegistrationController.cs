using Book_Swap_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Book_Swap_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response registration(Registration registration)
        {
            try
            {
                DataTable user = new DataTable();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());

                SqlCommand checkMail = new SqlCommand("Select * from [User] where(EmailId='" + registration.EmailId + "')", connection);
                SqlCommand command = new SqlCommand("INSERT INTO [User](UserName,UserKey,EmailId,IsActive,CreatedDate) Values('" + registration.UserName + "','" + registration.UserKey + "','" + registration.EmailId + "','" + 1 + "',GETDATE())", connection);
                
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(checkMail);
                da.Fill(user);
                int i = 0;
                if (user.Rows.Count == 0)
                {
                    i = command.ExecuteNonQuery();

                }
                connection.Close();
                if (i > 0)
                {
                    return new Response()
                    {
                        statusCode = System.Net.HttpStatusCode.OK,
                        statusMessage = "Data Inserted SucessFully"
                    };
                }
                return new Response()
                {
                    statusCode = System.Net.HttpStatusCode.BadRequest,
                    statusMessage = "Email already Exist"
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    statusCode = System.Net.HttpStatusCode.BadRequest,
                    statusMessage = string.Format("Creating an user failed. Exception details are: {0}", ex.Message)
                };
            }

        }

        [HttpPost]
        [Route("login")]
        public Response login(Registration registration)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
                SqlDataAdapter da = new SqlDataAdapter("select * from [User] where UserName = '" + registration.UserName + "' and  UserKey = '" + registration.UserKey + "' and EmailId = '" + registration.EmailId + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return new Response()
                    {
                        statusCode = System.Net.HttpStatusCode.OK,
                        statusMessage = "User Loged SucessFully"
                    };
                }
                else
                {
                    return new Response()
                    {
                        statusCode = System.Net.HttpStatusCode.BadRequest,
                        statusMessage = "Invalid User"
                    };
                }
            }
            catch (Exception ex)
            {

                return new Response()
                {
                    statusCode = System.Net.HttpStatusCode.BadRequest,
                    statusMessage = string.Format("Login user failed. Exception details are: {0}", ex.Message)
                };
            }

        }


        [HttpPost]
        [Route("Deleteuser")]     
        public Response Deleteuser(string email)
        {
            try
            {
                DataTable user = new DataTable();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
                SqlCommand checkMail = new SqlCommand("Select * from [User] where(isActive='" + 1 + "'and  EmailId='" + email + "')", connection);
                SqlCommand command = new SqlCommand("update [User] set IsActive = 0 where EmailId= '" + email + "'", connection);
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(checkMail);
                da.Fill(user);
                int i = 0;
                if (user.Rows.Count > 0)
                {
                    i = command.ExecuteNonQuery();

                }
                connection.Close();
                if (i > 0)
                {
                    return new Response()
                    {
                        statusCode = System.Net.HttpStatusCode.OK,
                        statusMessage = "User deleted Sucessfully"
                    };
                }
                return new Response()
                {
                    statusCode = System.Net.HttpStatusCode.BadRequest,
                    statusMessage = "User doesnt Exist"
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    statusCode = System.Net.HttpStatusCode.BadRequest,
                    statusMessage = string.Format("Deleting an user failed. Exception details are: {0}", ex.Message)
                };
            }

        }
    }
}
