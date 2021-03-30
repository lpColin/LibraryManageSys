using LibraryManageSys.DAL;
using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageSys.Controllers
{
    public class Common
    {
        public static List<SelectListItem> getBookTypeItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (var db = new LMSEntitys()) 
            {
                foreach (var item in db.Dictionarys.Where(o => o.Type == "BookType"))
                {
                    items.Add(new SelectListItem { Text = item.DisplayName, Value = item.Code });
                }
                items[0].Selected = true;
                return items;
            }
        }

        public static string GetDisplayName(string type, string code) 
        {
            using (var db = new LMSEntitys())
            {
                var obj = db.Dictionarys.Where(o => o.Type == type && o.Code == code);
                return obj.FirstOrDefault().DisplayName;
            }
        }
    }
}