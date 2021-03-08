using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.DAL
{
    public interface IBorrowItemDal:ICommonDal<BorrowItem>
    {
        bool judgeReader(int readerId);

        bool judgeBook(int bookId);

        void borrowBookSuccess(int readerId, int bookId);

        void returnBookAndReader(int bookId, int readerId);

        void CancelBookAndReader(int bookId, int readerId);
    }
}