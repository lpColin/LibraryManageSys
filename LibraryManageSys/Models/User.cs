using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{
    /// <summary>
    /// user entity created
    /// <remarks>create:2014.8.25</remarks>
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [Display(Name="用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度至少为6位")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [Display(Name = "邮箱地址")]
        [StringLength(50)]
        public string EmailAddress { get; set; }
    }
}