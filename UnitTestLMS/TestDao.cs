using LibraryManageSys.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.DAL
{
    [TestClass()]
    public class TestDao
    {
        [TestMethod]
        public void TestAddUser()
        {
            string s = "nihao";
            Assert.AreEqual("nihao",s);
            Console.Out.WriteLine(s);
        }

        [TestMethod]
        public void TestFindUserById() {
            int id = 1;
           // LMSEntitys db = ContextFactory.GetCurrentContext();
            LMSEntitys db = new LMSEntitys();
            //var user = db.users.Where(u => u.userId == id);
           var user = db.users.Where(u=>u.userId==id);
            foreach (var u in user)
            {
                Console.Out.WriteLine(u.userName + u.userId + u.password);
            }  
        }

        [TestMethod]
        public void TestUpdateUser() { 
            
        }

        [TestMethod]
        public void GetReadersByName() {
            string userName = "zhangyun";
            IReaderDal iRedaerDal = RepositoryFactory.ReaderRepository;
            var _list = iRedaerDal.FindList(a => a.readerName.Contains(userName), userName, true);
            foreach (var a in _list) {
                Console.Out.WriteLine(a.readerName);  
            }
        }

        
    }
}