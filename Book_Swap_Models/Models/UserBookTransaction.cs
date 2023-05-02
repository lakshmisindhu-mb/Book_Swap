using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class UserBookTransaction
{
    public int Id { get; set; }

    public int BorrowerId { get; set; }

    public int LenderId { get; set; }

    public int BookId { get; set; }

    public DateTime? BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string? Review { get; set; }

    public string? Amount { get; set; }
}
