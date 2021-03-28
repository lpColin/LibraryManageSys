using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{
    /// <summary>
    /// user login model 
    /// <remarks>create:2014.8.27</remarks>
    /// </summary>
    public class LoginViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} must be at least {2} characters long")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}