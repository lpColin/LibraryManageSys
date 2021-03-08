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
    [Table("tb_book")]
    public class Book
    {

        [Key]
        public int bookId { get; set; }

        [Required]
        [Display(Name="Book Name")]
        public string bookName { get; set; }

        [Display(Name = "Author")]
        public string author { get; set; }

        [Display(Name = "Publish")]
        public string publish { get; set; }
       
        [Display(Name = "Type")]
        [Required]
        public string type { get; set; }

        [Display(Name = "Amount")]
        public int amount { get; set; }

        [Display(Name = "CurrAmount")]
        public int currAmount { get; set; }

        [DataType(DataType.ImageUrl)]
        public string imageURL { get; set; }

        [Display(Name = "Introduction")]
        public string introduction { get; set; }

        [Display(Name = "AddTime")]
        [DataType(DataType.DateTime)]
        public DateTime addTime { get; set; }

        [Display(Name = "AddName")]
        public string addName { get; set; }


        public List<SelectListItem> getBookTypeItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Story", Value = "Story", Selected = true });
            items.Add(new SelectListItem { Text = "Prose", Value = "Prose", });
            items.Add(new SelectListItem { Text = "Poetry", Value = "Poetry", });
            items.Add(new SelectListItem { Text = "History", Value = "History", });
            items.Add(new SelectListItem { Text = "Medicine", Value = "Medicine", });
            items.Add(new SelectListItem { Text = "Technology", Value = "Technology", });
            return items;
        }
    }
}