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
    public class BorrowItemController : Controller
    {
        private LMSEntitys db = ContextFactory.GetCurrentContext();

        // GET: /BorrowItem/
        public ActionResult Index(string keyword = "", int page = 1)
        {
            var borrowitems = db.borrowItems.Include(b => b.book).Include(b => b.reader);
            if (!string.IsNullOrEmpty(keyword))
            {
                borrowitems = borrowitems.Where(o => o.book.bookName.Contains(keyword) || o.reader.readerName.Contains(keyword));
            }
            var borrowItemVideMode = new BorrowViewModel();
            borrowItemVideMode.BorrowItems = borrowitems.Where(b => b.status == Status.在借).OrderByDescending(p => p.burrowTime).ToPagedList(page, 5);
            return View(borrowItemVideMode);
        }

        // GET: /BorrowItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowItem borrowitem = db.borrowItems.Include(b => b.book).Include(b => b.reader).FirstOrDefault(o => o.borrowId == id.Value);
            if (borrowitem == null)
            {
                return HttpNotFound();
            }
            return View(borrowitem);
        }

        // GET: /BorrowItem/Create
        public ActionResult Create()
        {
            BorrowItem _borrowItem = new BorrowItem();
            _borrowItem.status = Status.在借;
            ViewBag.bookId = new SelectList(db.books, "bookId", "bookName");
            ViewBag.readerId = new SelectList(db.readers, "readerId", "readerName");

            return View(_borrowItem);
        }

        // POST: /BorrowItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "borrowId,status,burrowTime,ygBackTime,sjBackTime,borrowOper,backOper,bookId,readerId")] BorrowItem borrowitem)
        {
            borrowitem.sjBackTime = new DateTime(2000, 01, 01);
            borrowitem.burrowTime = DateTime.Now;
            borrowitem.ygBackTime = DateTime.Now.AddMonths(+3);
            if (Session["userName"] != null)
            {
                borrowitem.borrowOper = Session["userName"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

            borrowitem.status = Status.在借;
            ViewBag.bookId = new SelectList(db.books, "bookId", "bookName");
            ViewBag.readerId = new SelectList(db.readers, "readerId", "readerName");
            try
            {
                if (ModelState.IsValid)
                {
                    int _bookID = borrowitem.bookId;
                    int _readerID = borrowitem.readerId;
                    IBorrowItemDal BorrowItemDal = RepositoryFactory.BorrowRepository;
                    if (!BorrowItemDal.judgeBook(_bookID))
                    {
                        ModelState.AddModelError("bookId", "book amount is not enough");
                        return View(borrowitem);
                    }
                    if (!BorrowItemDal.judgeReader(_readerID))
                    {
                        ModelState.AddModelError("readerId", "balance or enable borrow amount is not enough");
                        return View(borrowitem);
                    }
                    BorrowItemDal.borrowBookSuccess(_readerID, _bookID);
                    db.borrowItems.Add(borrowitem);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.bookId = new SelectList(db.books, "bookId", "bookName", borrowitem.bookId);
                ViewBag.readerId = new SelectList(db.readers, "readerId", "readerName", borrowitem.readerId);
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(BorrowItemController), e);
                throw e;
            }
            return View(borrowitem);
        }

        // GET: /BorrowItem/Edit/5
        public ActionResult Return(int id)
        {
            BorrowItem borrowitem = db.borrowItems.Find(id);
            if (borrowitem == null)
            {
                LogHelper.WriteLog(typeof(BorrowItemController), "HttpNotFound");
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int? id)
        {
            IBorrowItemDal BorrowItemDal = RepositoryFactory.BorrowRepository;
            var borrowitems = db.borrowItems.Include(b => b.book).Include(b => b.reader);
            BorrowItem borrowitem = db.borrowItems.Find(id);
            if (borrowitem == null)
            {
                LogHelper.WriteLog(typeof(BorrowItemController), "HttpNotFound");
                return HttpNotFound();
            }
            try
            {
                BorrowItemDal.returnBookAndReader(borrowitem.bookId, borrowitem.readerId);
                borrowitem.status = Status.已还;
                if (Session["userName"] != null)
                {
                    borrowitem.backOper = Session["userName"].ToString();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }

                borrowitem.sjBackTime = DateTime.Now;
                db.Entry(borrowitem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(BorrowItemController), e);
            }
            return RedirectToAction("Index", "BorrowItem");
        }

        /// <summary>
        /// show Return records
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ReturnRecord(string keyword, int page = 1)
        {
            var borrowitems = db.borrowItems.Include(b => b.book).Include(b => b.reader);
            if (!string.IsNullOrEmpty(keyword))
            {
                borrowitems = borrowitems.Where(o => o.book.bookName.Contains(keyword) || o.reader.readerName.Contains(keyword));
            }
            var borrowItemVideMode = new BorrowViewModel();
            borrowItemVideMode.BorrowItems = borrowitems.Where(b => b.status == Status.已还).OrderByDescending(p => p.burrowTime).ToPagedList(page, 5);
            return View(borrowItemVideMode);
        }


        //Cancel borrow book record 
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowItem borrowitem = db.borrowItems.Find(id);
            if (borrowitem == null)
            {
                LogHelper.WriteLog(typeof(BorrowItemController), "HttpNotFound");
                return HttpNotFound();
            }
            return View("Cancel", borrowitem);
        }

        //deal with cancel operation
        public ActionResult Cancel2(int? id)
        {
            IBorrowItemDal BorrowItemDal = RepositoryFactory.BorrowRepository;
            var borrowitems = db.borrowItems.Include(b => b.book).Include(b => b.reader);
            BorrowItem borrowitem = db.borrowItems.Find(id);
            if (borrowitem == null)
            {
                LogHelper.WriteLog(typeof(BorrowItemController), "HttpNotFound");
                return HttpNotFound();
            }
            try
            {
                BorrowItemDal.CancelBookAndReader(borrowitem.bookId, borrowitem.readerId);
                borrowitem.status = Status.取消;
                if (Session["userName"] != null)
                {
                    borrowitem.backOper = Session["userName"].ToString();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
                borrowitem.sjBackTime = DateTime.Now;
                db.Entry(borrowitem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(BorrowItemController), e);
            }
            return RedirectToAction("ShowCancelRecord");
        }

        [HttpGet]
        /// <summary>
        /// Show Cancel Records
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ShowCancelRecord(int page = 1)
        {
            var borrowitems = db.borrowItems.Include(b => b.book).Include(b => b.reader);
            return View(borrowitems.Where(b => b.status == Status.取消).OrderBy(p => p.burrowTime).ToPagedList(page, 5));
        }

        // GET: /BorrowItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowItem borrowitem = db.borrowItems.Find(id);
            if (borrowitem == null)
            {
                return HttpNotFound();
            }
            return View(borrowitem);
        }

        // POST: /BorrowItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowItem borrowitem = db.borrowItems.Find(id);
            db.borrowItems.Remove(borrowitem);
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
