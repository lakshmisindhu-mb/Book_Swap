using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class BookGenreList
{
    public int Id { get; set; }

    public string GenreName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
