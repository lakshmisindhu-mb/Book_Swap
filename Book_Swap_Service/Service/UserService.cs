using Book_Swap_DL;
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
    public class UserService : IUserInterface
    {
        private readonly BookSwapContext bookSwapContext;
        private readonly IEncrypt _encrypt;
        public UserService(BookSwapContext bookSwapContext, IEncrypt encrypt)
        {
            this.bookSwapContext = bookSwapContext;
            _encrypt = encrypt;
        }

        public List<User> GetUserList()
        {
            return bookSwapContext.Users.ToList();
        }

        public User GetUserDetails(int bookID)
        {
            return bookSwapContext.Users.Find(bookID);
        }
        public bool CheckEmail(User emailCheck)
        {
            var Email = bookSwapContext.Users.Where(x => x.EmailId == emailCheck.EmailId).FirstOrDefault();
            return Email != null;
        }
        public string Register(User register)
        {
            try
            {
                register.UserName = register.UserName;
                register.EmailId = register.EmailId;
                register.UserKey = _encrypt.EncodePasswordToBase64(register.UserKey!);
                register.CreatedDate = DateTime.Now;
                register.UpdatedDate = DateTime.Now;
                register.IsActive = true;
                bookSwapContext.Users.Add(register);
                bookSwapContext.SaveChanges();
                return "Registration Successfull";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(User login)
        {
            try
            {
                var checkpasword=_encrypt.EncodePasswordToBase64(login.UserKey!);
                var checkcredentials=bookSwapContext.Users.Where(x => x.EmailId == login.EmailId && x.UserName==login.UserName && x.UserKey==checkpasword).FirstOrDefault();
                if (checkcredentials != null)
                {
                    return "User Login SuccessFully";
                }
                return "Invalid User";
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string ForgotPassword(User forgetPwd)
        {
            try
            {
                var userMail = bookSwapContext.Users.Where(x => x.EmailId == forgetPwd.EmailId).SingleOrDefault();
                string encryptPassword = _encrypt.EncodePasswordToBase64(forgetPwd.UserKey!);
                if (userMail != null)
                {
                    userMail.UserKey = encryptPassword;
                    userMail.UpdatedDate = DateTime.Now;
                    userMail.IsActive = true;
                    bookSwapContext.Entry(userMail).State = EntityState.Modified;
                    bookSwapContext.SaveChanges();
                    return "Password reset  Successfully";
                }
                return "Invalid Email";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateUser(User userList)
        {
            try
            {
                User user = bookSwapContext.Users.Where(y => y.Id == userList.Id).FirstOrDefault()!;
                string encryptPassword = _encrypt.EncodePasswordToBase64(user.UserKey!);
                user.UserName = userList.UserName;
                user.UpdatedDate = DateTime.Now;
                user.UserKey = encryptPassword;
                user.IsActive = true;
                bookSwapContext.Entry(user).State = EntityState.Modified;
                bookSwapContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteUser(int Id)
        {
            try
            {
                User user = bookSwapContext.Users.Where(y => y.Id == Id).FirstOrDefault()!;
                user.IsActive = false;
                bookSwapContext.Entry(user).State = EntityState.Modified;
                bookSwapContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User SearchUser(string username, string email )
        {
            User user = new();
            try
            {
                if (email == null && username != null)
                {
                    user = bookSwapContext.Users.Where(y => y.UserName == username).FirstOrDefault()!;
                }
                else if (email != null && username == null)
                {
                    user = bookSwapContext.Users.Where(y => y.EmailId == email).FirstOrDefault()!;
                }
                else if (email != null && username != null)
                {
                    user = bookSwapContext.Users.Where(y => y.EmailId == email && y.UserName == username).FirstOrDefault()!;
                }

                return user;
            }
            catch(Exception ex)
            {
                throw ;

            }
        }

        public RateUserResponse RateUser(RateUserRequest request)
        {
            if (request.FromUserId > 0 && request.BorrowerId > 0 && request.Rating > 0)
            {
                var existingRating = bookSwapContext.UserRatings.Where(x => x.BorrowerId == request.BorrowerId && x.LenderId == request.FromUserId).FirstOrDefault();
                if (existingRating != null && existingRating.BorrowerId > 0 && existingRating.LenderId > 0)
                {
                    bookSwapContext.Entry(existingRating).State = EntityState.Deleted;
                    bookSwapContext.SaveChanges();

                    AddRatingDB(request);
                }
                else
                {
                    var ifUserBorrowed = bookSwapContext.UserBookTransactions.Where(x => x.LenderId == request.FromUserId && x.BorrowerId == request.BorrowerId && x.ReturnDate < DateTime.Now).Any();
                    if (ifUserBorrowed)
                    {
                        AddRatingDB(request);
                    }
                    else
                    {
                        return new RateUserResponse { Message = "transactions not found with given values", StatusCode = System.Net.HttpStatusCode.NotFound };
                    }
                }

            }
            else
            {
                return new RateUserResponse { Message = "Incorrect Values passed", StatusCode = System.Net.HttpStatusCode.NotAcceptable };
            }
            return new RateUserResponse { Message = "success", StatusCode = System.Net.HttpStatusCode.OK };
        }

        private void AddRatingDB(RateUserRequest request)
        {
            var ratings = new UserRatings { LenderId = request.FromUserId, BorrowerId = request.BorrowerId, Rating = request.Rating };
            bookSwapContext.UserRatings.Add(ratings);
            bookSwapContext.SaveChanges();

            var rating = bookSwapContext.UserRatings.Where(x => x.BorrowerId == request.BorrowerId).Average(x => x.Rating);

            var user = bookSwapContext.Users.Where(y => y.Id == request.BorrowerId).FirstOrDefault();
            if (user != null)
            {
                user.UpdatedDate = DateTime.Now;
                user.AverageRating = Math.Round(rating, 1); ;
                bookSwapContext.Entry(user).State = EntityState.Modified;
                bookSwapContext.SaveChanges();
            }

        }


    }
}
