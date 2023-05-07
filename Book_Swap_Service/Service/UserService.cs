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
        public UserService(BookSwapContext bookSwapContext)
        {
            this.bookSwapContext = bookSwapContext;
        }

        public void UpdateUser(User userList)
        {
            try
            {
                User user = bookSwapContext.Users.Where(y => y.Id == userList.Id).FirstOrDefault()!;
                user.UpdatedDate = userList.UpdatedDate;
                user.UserKey = userList.UserKey;
                bookSwapContext.Entry(user).State = EntityState.Modified;
                bookSwapContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteUser(int Id)
        {
            try
            {
                User user = bookSwapContext.Users.Where(y => y.Id == Id).FirstOrDefault()!;
                bookSwapContext.Entry(user).State = EntityState.Deleted;
                bookSwapContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void SearchUser(int Id)
        {
            //BookList book = bookSwapContext.BookLists.Where(y => y.Id == bookList.Id).FirstOrDefault()!; 
            //book.BookName = bookList.BookName;
            //book.GenreId = bookList.GenreId;    
            //book.Author = bookList.Author;
            //book.Publisher = bookList.Publisher;
            //book.Edition = bookList.Edition;
            //book.ReleaseDate = bookList.ReleaseDate;
            //book.ModifiedDate = DateTime.Now;
            //bookSwapContext.Entry(book).State = EntityState.Modified;
            //bookSwapContext.SaveChanges();
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

            User user = bookSwapContext.Users.Where(y => y.Id == request.BorrowerId).FirstOrDefault()!;
            user.UpdatedDate = DateTime.Now;
            user.AverageRating = rating;
            bookSwapContext.Entry(user).State = EntityState.Modified;
            bookSwapContext.SaveChanges();
        }


    }
}
