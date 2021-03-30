using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{
    /// <summary>
    /// reader entity created
    /// <remarks>create:2014.8.25</remarks>
    /// </summary>
    public class Reader
    {
        [Key]
        public int readerId { get; set; }

        [Required(ErrorMessage="readerName not null")]
        [Display(Name="读者名字")]
        public string readerName { get; set; }

        [Display(Name = "电话号码")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNum { get; set; }

        [UIHint("性别")]
        public Gender Gender { get; set; }

        //[DataType(DataType.EmailAddress,ErrorMessage="email address format is incorrect")]
        [UIHint("邮箱")]
        public string email { get; set; }

        [Display(Name = "可用余额")]
        public double balance { get; set; }

        [Required]
        [Display(Name = "可借数量")]
        public int enableBorrowNum { get; set; }

        [Required]
        [Display(Name = "创建人")]
        public string createName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "创建时间")]
        public DateTime createTime { get; set; }

    }
    public enum Gender { Male, Female }
}