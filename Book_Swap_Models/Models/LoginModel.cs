using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Swap_Models.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Please enter user name.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Name")]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(10)]
        public string Password { get; set; }
    }
}
