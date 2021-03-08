using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{
    public class UserViewModel
    {
        public int userId { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string userName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} must be at least {2} characters long")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}