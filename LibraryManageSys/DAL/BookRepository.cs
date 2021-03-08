using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.DAL
{
    public class BookRepository:CommonDal<Book>,IBookDal
    {
        public List<Book> FindBookList(string bookName, string author)
        {
            List<Book> bookList =null;
            if (!string.IsNullOrEmpty(bookName))
            {   
                 if (!string.IsNullOrEmpty(author))
                 {
                     bookList = nContext.books.Where(b => b.bookName.Contains(bookName) && b.author.Contains(author)).ToList();
                 }
                 else {
                     bookList = nContext.books.Where(b => b.bookName.Contains(bookName)).ToList();
                 }
            }else{
                if(!string.IsNullOrEmpty(author)){
                    bookList = nContext.books.Where(b => b.author.Contains(author)).ToList();
                }else{
                    bookList = nContext.books.ToList();
                }
            }
            return bookList;
        }
    }
}