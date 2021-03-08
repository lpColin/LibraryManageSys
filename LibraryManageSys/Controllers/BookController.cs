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
        public ViewResult Index(string sortOrder, string bookName, string author, string CurrentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.bookName = bookName;
            ViewBag.author = author;
            List<SelectListItem> items = new Book().getBookTypeItems();
            this.ViewData["list"] = items;
            ViewBag.AmountSortParm = String.IsNullOrEmpty(sortOrder) ? "Amount_desc" : "";
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "Type_Asc" : "";
            ViewBag.CurrAmountSortParm = String.IsNullOrEmpty(sortOrder) ? "CurrAmount_desc" : "";
            ViewBag.AddTimeSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            IBookDal ibookDao = RepositoryFactory.BookRepository;
            //_bookList = ibookDao.FindBookList(bookName, author);
            if (bookName != null || author != null)
            { 
                page = 1;
            }else{
                bookName = sortOrder;
                 }
            var books = from s in ibookDao.FindBookList(bookName, author)
                        select s;
            switch (sortOrder)
            {
                case "Amount_desc":
                    books = books.OrderByDescending(s => s.amount);
                    break;
                case "Type_Asc":
                    books = books.OrderBy(s => s.type);
                    break;
                case "CurrAmount_desc":
                    books = books.OrderByDescending(s => s.currAmount);
                    break;
                case "Date":
                    books = books.OrderBy(s => s.addTime);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(s => s.addTime);
                    break;
                default:
                    books = books.OrderBy(s => s.bookName);
                    break;
            }
            int pageSize = 5; int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(FormCollection Collection)
        //{
        //    var _bookList = new  List<Book>();

        //    try
        //    {
        //        string _bookName = Collection["bookName"];
        //        string _author = Collection["author"];
        //        List<SelectListItem> items = new Book().getBookTypeItems();
        //        this.ViewData["list"] = items;
        //        IBookDal ibookDao = RepositoryFactory.BookRepository;
        //        _bookList = ibookDao.FindBookList(_bookName, _author);
        //    }
        //    catch (Exception e) {
        //        LogHelper.WriteLog(typeof(BookController),e);
        //        throw e;
        //    }
        //    return View(_bookList.ToPagedList(1,5));
        //}

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

        // GET: /Book/Create
        public ActionResult Create()
        {
            Book book = new Book();
            List<SelectListItem> items = new Book().getBookTypeItems();
            this.ViewData["list"] = items;
            book.amount = 2;
            book.type = "Story";
            return View(book);
        }

        // POST: /Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                List<SelectListItem> items = new Book().getBookTypeItems();
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
        }

        // GET: /Book/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> items = new Book().getBookTypeItems();
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

        // POST: /Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bookId,bookName,author,publish,type,amount,currAmount,imageURL,introduction,addTime,addName")] Book book)
        {
            List<SelectListItem> items = new Book().getBookTypeItems();
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

        // GET: /Book/Delete/5
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

        // POST: /Book/Delete/5
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
