using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class BookRequest
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? BorrowerId { get; set; }

    public bool? Approved { get; set; }

    public int? OwnerId { get; set; }
}
