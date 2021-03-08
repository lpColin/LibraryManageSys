using LibraryManageSys.Models;
using LMS.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManageSys.DAL
{
    public class BorrowItemRepository:CommonDal<BorrowItem> ,IBorrowItemDal
    {
        public bool judgeReader(int readerId)
        {
            try
            {
                var _b = nContext.readers.FirstOrDefault(a => a.readerId == readerId);
                Reader _reader = new Reader();
                _reader = _b;
                double _balance = _reader.balance;
                int _amount = _reader.enableBorrowNum;
                if (_balance < 1 || _amount < 1)
                {
                    return false;
                }
            }
            catch (Exception e) {
                LogHelper.WriteLog(typeof(BorrowItemRepository), e);
                throw e;
            }
            return true;
        }

        public bool judgeBook(int bookId)
        {
            try
            {
                var _b = nContext.books.FirstOrDefault(a => a.bookId == bookId);
                Book _book = new Book();
                _book = _b;
                int _bookCurrAmount = _book.currAmount;
                if (_bookCurrAmount < 1)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(BorrowItemRepository), e);
                throw e;
            }
            return true;
        }

        public void borrowBookSuccess(int readerId, int bookId) {
            try
            {
                var _r = nContext.readers.FirstOrDefault(a => a.readerId == readerId);
                Reader _reader = new Reader();
                _reader = _r;
                _reader.balance = _reader.balance - 1;
                _reader.enableBorrowNum = _reader.enableBorrowNum - 1;
                nContext.Entry(_reader).State = EntityState.Modified;

                var _b = nContext.books.FirstOrDefault(a => a.bookId == bookId);
                Book _book = new Book();
                _book = _b;
                _book.currAmount = _book.currAmount - 1;
                nContext.Entry(_book).State = EntityState.Modified;
                nContext.SaveChanges();
            }
            catch (Exception e) {
                LogHelper.WriteLog(typeof(BorrowItemRepository), e);
                throw e;
            }
        }


        /// <summary>
        /// when return book, we need to return book amount and reader current enable amount
        /// </summary>
        /// <param name="bookId">bookId</param>
        /// <param name="readerId">readerId</param>
        public void returnBookAndReader(int bookId, int readerId) {

            try
            {
                var _b = nContext.books.FirstOrDefault(a => a.bookId == bookId);
                var _r = nContext.readers.FirstOrDefault(a => a.readerId == readerId);
                Book _book = new Book();
                Reader _reader = new Reader();
                _book = _b;
                _reader = _r;
                _book.currAmount = _book.currAmount + 1;
                _reader.enableBorrowNum = _reader.enableBorrowNum + 1;
                nContext.Entry(_book).State = EntityState.Modified;
                nContext.Entry(_reader).State = EntityState.Modified;
                nContext.SaveChanges();
            }
            catch (Exception e) {
                LogHelper.WriteLog(typeof(BorrowItemRepository), e);
                throw e;
            }        
        }

        /// <summary>
        /// when Cancel book, we need to return book amount and reader current enable amount
        /// </summary>
        /// <param name="bookId">bookId</param>
        /// <param name="readerId">readerId</param>
        public void CancelBookAndReader(int bookId, int readerId)
        {

            try
            {
                var _b = nContext.books.FirstOrDefault(a => a.bookId == bookId);
                var _r = nContext.readers.FirstOrDefault(a => a.readerId == readerId);
                Book _book = new Book();
                Reader _reader = new Reader();
                _book = _b;
                _reader = _r;
                _book.currAmount = _book.currAmount + 1;
                _reader.enableBorrowNum = _reader.enableBorrowNum + 1;
                _reader.balance = _reader.balance + 1;
                nContext.Entry(_book).State = EntityState.Modified;
                nContext.Entry(_reader).State = EntityState.Modified;
                nContext.SaveChanges();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(BorrowItemRepository), e);
                throw e;
            }
        }
    }
}