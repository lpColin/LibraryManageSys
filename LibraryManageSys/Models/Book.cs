using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageSys.Models
{
    /// <summary>
    /// bookInfo entity created
    /// <remarks>created:2014.8.25</remarks>
    /// </summary>
    /// 
    public class Book
    {

        [Key]
        public int bookId { get; set; }

        [Required]
        [Display(Name="书名")]
        public string bookName { get; set; }

        [Display(Name = "作者")]
        public string author { get; set; }

        [Display(Name = "出版社")]
        public string publish { get; set; }
       
        [Display(Name = "类型")]
        [Required]
        public string type { get; set; }

        [Display(Name = "数量")]
        public int amount { get; set; }

        [Display(Name = "剩余数量")]
        public int currAmount { get; set; }

        [DataType(DataType.ImageUrl)]
        public string imageURL { get; set; }

        [Display(Name = "介绍")]
        public string introduction { get; set; }

        [Display(Name = "添加时间")]
        [DataType(DataType.DateTime)]
        public DateTime addTime { get; set; }

        [Display(Name = "操作人")]
        public string addName { get; set; }
    }
}