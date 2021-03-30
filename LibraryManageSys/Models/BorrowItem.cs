using PagedList;
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
    public class BorrowItem
    {
        [Key]
        public int borrowId { get; set;}

        [Required]
        [UIHint("Enum")]
        [Display(Name="借书状态")]
        public Status status { get; set; }

        [Display(Name = "借书时间")]
        public DateTime burrowTime { get; set;}

        [Display(Name = "最后还书时间")]
        public DateTime ygBackTime { get; set; }

        [Display(Name = "实际还书时间")]
        public DateTime sjBackTime { get; set; }

        [Display(Name = "借书操作人")]
        public string borrowOper { get; set; }

        [Display(Name = "还书操作人")]
        public string backOper { get; set; }

        public int bookId { get; set; }

        public int readerId { get; set; }

        public virtual Book book { get; set; }

        public virtual Reader reader { get; set; }
        
    }
    public enum Status {Borrow,Return,Cancel}
    public class BorrowViewModel
    {
        public IPagedList<LibraryManageSys.Models.BorrowItem> BorrowItems { get; set; }

        public BorrowItem BorrowItem { get; set; }
    }
}

