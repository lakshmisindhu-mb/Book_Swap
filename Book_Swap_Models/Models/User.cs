using System;
using System.Collections.Generic;

namespace Book_Swap_Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string? UserKey { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsActive { get; set; }
}
