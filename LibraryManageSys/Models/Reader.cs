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
    [Table("tb_reader")]
    public class Reader
    {
        [Key]
        public int readerId { get; set; }

        [Required(ErrorMessage="readerName not null")]
        [Display(Name="Reader Name")]
        public string readerName { get; set; }

        [Display(Name = "Tell")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNum { get; set; }

        [UIHint("Enum")]
        public Gender Gender { get; set; }

        //[DataType(DataType.EmailAddress,ErrorMessage="email address format is incorrect")]
        [UIHint("EmailAddress")]
        public string email { get; set; }

        public double balance { get; set; }

        [Required]
        [Display(Name = "Available Amount")]
        public int enableBorrowNum { get; set; }

        [Required]
        public string createName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime createTime { get; set; }

    }
    public enum Gender { Male, Female }
}