using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class UserRating
{
    public int Id { get; set; }

    public int? LenderId { get; set; }

    public int? BorrowerId { get; set; }

    public int? Rating { get; set; }
}
