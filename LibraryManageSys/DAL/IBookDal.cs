using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.DAL
{
    public interface IBookDal : ICommonDal<Book>
    {
        List<Book> FindBookList(string bookName, string author);
    }
}