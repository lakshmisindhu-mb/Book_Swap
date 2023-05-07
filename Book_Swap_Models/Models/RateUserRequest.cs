using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Models.Models
{
    public class RateUserRequest
    {
        public int FromUserId { get; set; }
        public int BorrowerId { get; set; }
        public int Rating { get; set; }

    }
}
