using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class GetUserRating
{
    public int Id { get; set; }

    public string LenderName { get; set; } = null!;

    public string BorrowerName { get; set; } = null!;

    public int? Rating { get; set; }
}
