using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Book_Swap_Models.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [StringLength(32)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [StringLength(32)]
        public string ConfirmPassword { get; set; }
    }
}
