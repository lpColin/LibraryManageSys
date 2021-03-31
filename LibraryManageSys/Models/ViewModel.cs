using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{

    /// <summary>
    /// 试图模型
    /// </summary>
    public class UserViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度至少为6位")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度至少为6位")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "姓名")]
        public string DisplayName { get; set; }

        [Display(Name = "邮箱地址")]
        public string EmailAddress { get; set; }
    }

    public class RegisterAndLoginViewModel 
    {
        public UserViewModel UserViewModel { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; }
    }

    public class ModifyPassword 
    {
        [Required]
        [Display(Name = "旧密码")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
    }
}