using System.ComponentModel.DataAnnotations;

public class WishListBookModel
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string EmailId { get; set; }

    [Required]
    [MaxLength(100)]
    public string BookName { get; set; }
    
    [MaxLength(50)]
    public string Edition { get; set; }

    [Required]
    [MaxLength(100)]
    public string Author { get; set; }

    [MaxLength(100)]
    public string Publisher { get; set; }

    
    [MaxLength(20)]
    public string Phone { get; set; }
}
