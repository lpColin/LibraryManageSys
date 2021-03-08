using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.DAL
{
    /// <summary>
    /// 简单工厂
    /// <remarks>创建：2014.8.22</remarks>
    /// </summary>
    public static class RepositoryFactory
    {
        public static IUserDal UserRepository { get { return new UserRepository(); } }
        public static IReaderDal ReaderRepository { get { return new ReaderRepository(); } }
        public static IBookDal BookRepository { get { return new BookRepository(); } }
        public static IBorrowItemDal BorrowRepository { get { return new BorrowItemRepository(); } }
    }
}