using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class GetUserBookTransaction
{
    public int UserBookTransactionId { get; set; }

    public string BorrowerName { get; set; } = null!;

    public string LenderName { get; set; } = null!;

    public string BookName { get; set; } = null!;

    public DateTime? BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string? Review { get; set; }

    public string? Amount { get; set; }
}
