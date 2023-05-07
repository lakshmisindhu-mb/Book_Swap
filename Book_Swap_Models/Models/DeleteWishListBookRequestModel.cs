using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Models.Models
{
    public class DeleteWishListBookRequestModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string BookName { get; set; }
    }
}
