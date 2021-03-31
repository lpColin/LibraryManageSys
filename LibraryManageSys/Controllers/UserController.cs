﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManageSys.Models;
using LibraryManageSys.BLL;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using LibraryManageSys.DAL;
using LMS.Common;
using System.Text;

namespace LibraryManageSys.Controllers
{
    public class UserController : Controller
    {
        private LMSEntitys db = ContextFactory.GetCurrentContext();
        private UserService userService = new UserService();

        // GET: /User/
        public ActionResult UserList()
        {
            return View(db.users.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterAndLoginViewModel userViewModel)
        {
            try
            {
                User user = new User() { UserName = userViewModel.RegisterViewModel.UserName, Password = userViewModel.RegisterViewModel.Password };
                if (ModelState.IsValid)
                {
                    if (userService.Exist(user.UserName))
                    {
                        ModelState.AddModelError("UserName", "用户名已存在！");
                        return View("LoginAndRegister", userViewModel);
                    }

                    else if (user != null)
                    {
                        userService.Add(user);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "error");
                        return View("LoginAndRegister", userViewModel);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(UserController), e);
                throw e;
            }
            return View(userViewModel);
        }

        public ActionResult Login()
        {
            if (Session["userName"] != null)
            {
                return RedirectToAction("Index", "Book");
            }
            else 
            {
                return View("LoginAndRegister");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(RegisterAndLoginViewModel Loginuser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _user = userService.Find(Loginuser.UserViewModel.UserName);

                    if (_user != null && Loginuser.UserViewModel.Password == _user.Password)
                    {
                        Session.Add("user", _user);
                        Session["userName"] = _user.DisplayName;
                        return RedirectToAction("Index", "Book");
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户名或者密码不匹配！");
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(UserController), e);
                throw e;
            }
            return View("LoginAndRegister", Loginuser);
        }

        // GET: /User/Edit/5
        public ActionResult EditPassword()
        {
            User loginuser = new User();
            if (Session["user"] != null)
            {
                loginuser = (User)Session["user"];
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            if (loginuser.UserId.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (loginuser == null)
            {
                return HttpNotFound();
            }
            return View(loginuser);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(ModifyPassword LogionUser)
        {
            if (ModelState.IsValid)
            {
                if (LogionUser != null)
                {
                    var sessionUser = (User)Session["user"];
                    sessionUser.Password = LogionUser.NewPassword;
                    userService.Update(sessionUser);
                }
                return RedirectToAction("Index");
            }
            return View(LogionUser);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "User");
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
