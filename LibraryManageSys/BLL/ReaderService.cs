using LibraryManageSys.DAL;
using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.BLL
{
    public class ReaderService : IBaseService<Reader>,IReaderService
    {
        public bool Exist(string userName)
        {
            throw new NotImplementedException();
        }

        public User Find(int userID)
        {
            throw new NotImplementedException();
        }

        public User Find(string userName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order)
        {
            throw new NotImplementedException();
        }


        public Reader Add(Reader Entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Reader Entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Reader Entity)
        {
            throw new NotImplementedException();
        }
    }
}