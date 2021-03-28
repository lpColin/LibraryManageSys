using LibraryManageSys.BLL;
using LibraryManageSys.DAL;
using LibraryManageSys.Models;
using LMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageSys.Controllers
{
    public class HomeController : Controller
    {
        private LMSEntitys db = ContextFactory.GetCurrentContext();
        private UserService userService = new UserService();

        public ActionResult Index()
        {
            if (Session["userName"] != null)
            {
                return RedirectToAction("Index", "Book");
            }
            else 
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "About System";

            return View();
        }
    }
}