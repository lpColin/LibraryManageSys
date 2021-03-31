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
    public class ReaderController : Controller
    {
        private LMSEntitys db = ContextFactory.GetCurrentContext();

        // GET: /Reader/
        public ActionResult Index(string keyword, int page = 1)
        {
            var viewData = db.readers.OrderByDescending(o=>o.createTime);
            if (!string.IsNullOrEmpty(keyword)) 
            {
                viewData.Where(o => o.readerName.Contains(keyword));
            }
            return View(viewData.ToPagedList(page,5) );
        }

        //get UserNamer from View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection Collection)
        {
            string readerName = Collection["readerName"];
            List<Reader> readers = new List<Reader>();
            IReaderDal iRedaerDal = RepositoryFactory.ReaderRepository;
            var _list = iRedaerDal.FindList(a => a.readerName.Contains(readerName), "readerName", true).ToList();
            return View(_list.ToPagedList(1,5));          
        }

        // GET: /Reader/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = db.readers.Find(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        // GET: /Reader/Create
        public ActionResult Create()
        {
            Reader reader = new Reader();
            try
            {
                if (Session["userName"] != null)
                {
                    reader.createName = Session["userName"].ToString();
                }
                else {
                    return RedirectToAction("Login","User");
                }    
            }
            catch (Exception e) {
                LogHelper.WriteLog(typeof(ReaderController),e);
                throw e;
            }
            return View(reader);
        }

        // POST: /Reader/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reader reader)
        {
            reader.createTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    db.readers.Add(reader);
                    db.SaveChanges();
                }
                catch (Exception e) {
                    LogHelper.WriteLog(typeof(ReaderController), e);
                    throw e;
                }
                
                return RedirectToAction("Index");
            }

            return View(reader);
        }

        // GET: /Reader/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = db.readers.Find(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        // POST: /Reader/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="readerId,readerName,phoneNum,Gender,email,balance,enableBorrowNum,createName,createTime")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reader).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reader);
        }

        // GET: /Reader/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = db.readers.Find(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        // POST: /Reader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reader reader = db.readers.Find(id);
            db.readers.Remove(reader);
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
