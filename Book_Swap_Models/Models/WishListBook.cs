using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class WishListBook
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? EmailId { get; set; }

    public string? BookName { get; set; }

    public string? Edition { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public string? Phone { get; set; }

    public DateTime? WishlistedDate { get; set; }

    public DateTime? DeadLineDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
