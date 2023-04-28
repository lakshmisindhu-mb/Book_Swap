using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class BookList
{
    public int Id { get; set; }

    public string BookName { get; set; } = null!;

    public int GenreId { get; set; }

    public string? Publisher { get; set; }

    public string? Author { get; set; }

    public string? Edition { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
