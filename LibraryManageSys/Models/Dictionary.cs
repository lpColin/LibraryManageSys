using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{
    public class Dictionary
    {
        [Key]
        [Required]
        [Description("主键 Id")]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        [MaxLength(500)]
        public string Remark { get; set; }
    }
}