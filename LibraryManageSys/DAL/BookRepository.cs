using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.DAL
{
    public class BookRepository:CommonDal<Book>,IBookDal
    {
        public List<Book> FindBookList(string keywords)
        {
            List<Book> bookList =null;
            bookList = nContext.books.ToList();
            if (!string.IsNullOrEmpty(keywords)) 
            {
                bookList = bookList.Where(o => o.author.Contains(keywords) || o.bookName.Contains(keywords)).ToList();
            }
            return bookList;
        }
    }
}