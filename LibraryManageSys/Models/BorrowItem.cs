using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{
    /// <summary>
    /// borrow Item entity created
    /// <remarks>created:2014.8.25</remarks>
    /// </summary>
    /// 
    [Table("tb_borrowItem")]
    public class BorrowItem
    {
        [Key]
        public int borrowId { get; set;}

        [Required]
        [UIHint("Enum")]
        [Display(Name="Borrow Status")]
        public Status status { get; set; }

        public DateTime burrowTime { get; set;}

        [Display(Name = "Preset BackTime")]
        public DateTime ygBackTime { get; set; }

        [Display(Name = "Fact BackTime")]
        public DateTime sjBackTime { get; set; }

        public string borrowOper { get; set; }

        public string backOper { get; set; }

        public int bookId { get; set; }

        public int readerId { get; set; }

        public virtual Book book { get; set; }

        public virtual Reader reader { get; set; }
        
    }
    public enum Status {Borrow,Return,Cancel}
}