using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManageSys.Models;
using LibraryManageSys.DAL;
using LMS.Common;
using PagedList;

namespace LibraryManageSys.Controllers
{
    [HandleError (ExceptionType = typeof(System.Data.DataException), View = "DataBaseError")]
    public class BookController : Controller
    {
        private LMSEntitys db = ContextFactory.GetCurrentContext();

        [HttpGet]
        // GET: /Book/
        public ViewResult Index(string sortOrder, string keyword,  string CurrentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.keyword = keyword;
            List<SelectListItem> items = Common.getBookTypeItems();
            this.ViewData["list"] = items;
            ViewBag.AmountSortParm = String.IsNullOrEmpty(sortOrder) ? "Amount_desc" : "";
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "Type_Asc" : "";
            ViewBag.CurrAmountSortParm = String.IsNullOrEmpty(sortOrder) ? "CurrAmount_desc" : "";
            ViewBag.AddTimeSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            IBookDal ibookDao = RepositoryFactory.BookRepository;

            var books = ibookDao.FindBookList(keyword);
            switch (sortOrder)
            {
                case "Amount_desc":
                    books = books.OrderByDescending(s => s.amount).ToList();
                    break;
                case "Type_Asc":
                    books = books.OrderBy(s => s.type).ToList();
                    break;
                case "CurrAmount_desc":
                    books = books.OrderByDescending(s => s.currAmount).ToList();
                    break;
                case "Date":
                    books = books.OrderBy(s => s.addTime).ToList();
                    break;
                case "date_desc":
                    books = books.OrderByDescending(s => s.addTime).ToList();
                    break;
                default:
                    books = books.OrderBy(s => s.bookName).ToList();
                    break;
            }
            for (int i = 0; i < books.Count(); i++)
            {
                var type = books[i].type;
                books[i].type = Common.GetDisplayName("BookType", type);
            }
            int pageSize = 5; int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Create()
        {
            Book book = new Book();
            List<SelectListItem> items = Common.getBookTypeItems();
            this.ViewData["list"] = items;
            book.amount = 2;
            return View(book);
        }

        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                List<SelectListItem> items = Common.getBookTypeItems();
                this.ViewData["list"] = items;

                string Type = Request.Form["list"];
                if (Type == null)
                {
                    ModelState.AddModelError("type", "not null");
                }
                else
                {
                    book.type = Type;
                }
                if (Session["userName"] != null)
                {
                    book.addName = Session["userName"].ToString();
                }
                else {
                    return RedirectToAction("Login", "User");
                } 
                
                book.addTime = DateTime.Now;
                if (ModelState.IsValid)
                {
                    book.currAmount = book.amount;
                    db.books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e) {
                LogHelper.WriteLog(typeof(BookController), e);
                throw e;
            }
            return View(book);
            //return PartialView("EditorTemplates/Dialog", book);
        }

        // GET: /Book/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> items = Common.getBookTypeItems();
            this.ViewData["list"] = items;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            List<SelectListItem> items = Common.getBookTypeItems();
            this.ViewData["list"] = items;
            string Type = Request.Form["list"];
            book.type = Type;
            if (ModelState.IsValid)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.books.Find(id);
            db.books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
