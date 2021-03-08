using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryManageSys.Models;

namespace LibraryManageSys.DAL
{
    public class LMSInitializer : DropCreateDatabaseIfModelChanges<LMSEntitys>
    {

        protected override void Seed(LMSEntitys context)
        {
            var users = new List<User>() { 
                new  User {userId = 1,userName="Admin",password="123456"},
                new  User {userId = 2,userName="Tom",password="123456"},
                new  User {userId = 3,userName="Jack",password="1234567"},
                new  User {userId = 4,userName="Rose",password="123456"}
            };
            users.ForEach(s =>context.users.Add(s));
            context.SaveChanges();

            var readers = new List<Reader>(){
                new Reader {readerId=1,Gender=Gender.Male,readerName="zhangyun",phoneNum="18862241924",enableBorrowNum=4,email="lupeng@163.com",balance=200,createTime=DateTime.Now,createName="Tom"},
                new Reader {readerId=2,Gender=Gender.Female,readerName="zhangyun",phoneNum="18862241924",enableBorrowNum=4,email="lupeng@163.com",balance=200,createTime=DateTime.Now,createName="Tom"} 
            };
            readers.ForEach(s => context.readers.Add(s));
            context.SaveChanges();

            var books = new List<Book>(){
                new Book{bookId=1,bookName="西游记",type="literature",publish="shandong",author="wuchengen",amount=10,currAmount=5,addTime=DateTime.Now,addName="Tom",introduction="very good"},
                new Book{bookId=2,bookName="红楼梦",type="literature",publish="shandong",author="wuchengen",amount=10,currAmount=5,addTime=DateTime.Now,addName="Tom",introduction="very good"}
            };
            books.ForEach(s=>context.books.Add(s));
            context.SaveChanges();

            var borrowItems = new List<BorrowItem>(){
                new BorrowItem{borrowId=1,bookId=1,readerId=1,status=Status.Borrow,borrowOper="Tom",burrowTime=DateTime.Now,sjBackTime=DateTime.Now,ygBackTime=DateTime.Now.AddMonths(+3)},
                new BorrowItem{borrowId=2,bookId=2,readerId=2,status=Status.Borrow,borrowOper="Tom",burrowTime=DateTime.Now,sjBackTime=DateTime.Now,ygBackTime=DateTime.Now.AddMonths(+3)}
            };
            borrowItems.ForEach(s => context.borrowItems.Add(s));
            context.SaveChanges();

        }
    }
}