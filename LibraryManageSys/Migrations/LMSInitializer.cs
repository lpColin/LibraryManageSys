using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryManageSys.Models;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;

namespace LibraryManageSys.Migrations
{
    internal sealed class LMSInitializer : DbMigrationsConfiguration<LMSEntitys>
    {
        public LMSInitializer()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }
        protected override void Seed(LMSEntitys context)
        {
            var users = new List<User>() {
                new  User {userId = 1,userName="Admin",password="123456"}
            };
            users.ForEach(s => context.users.AddOrUpdate(s));

            var readers = new List<Reader>(){
                new Reader {readerId=1,Gender=Gender.Male,readerName="zhangyun",phoneNum="18862241924",enableBorrowNum=4,email="lupeng@163.com",balance=200,createTime=DateTime.Now,createName="Tom"},
                new Reader {readerId=2,Gender=Gender.Female,readerName="zhangyun",phoneNum="18862241924",enableBorrowNum=4,email="lupeng@163.com",balance=200,createTime=DateTime.Now,createName="Tom"}
            };
            readers.ForEach(s => context.readers.AddOrUpdate(s));

            var books = new List<Book>(){
                new Book{bookId=1,bookName="西游记",type="Novel",publish="shandong",author="wuchengen",amount=10,currAmount=5,addTime=DateTime.Now,addName="Tom",introduction="very good"},
                new Book{bookId=2,bookName="红楼梦",type="Novel",publish="shandong",author="wuchengen",amount=10,currAmount=5,addTime=DateTime.Now,addName="Tom",introduction="very good"}
            };
            books.ForEach(s => context.books.AddOrUpdate(s));

            var borrowItems = new List<BorrowItem>(){
                new BorrowItem{borrowId=1,bookId=1,readerId=1,status=Status.Borrow,borrowOper="Tom",burrowTime=DateTime.Now,sjBackTime=DateTime.Now,ygBackTime=DateTime.Now.AddMonths(+3)},
                new BorrowItem{borrowId=2,bookId=2,readerId=2,status=Status.Borrow,borrowOper="Tom",burrowTime=DateTime.Now,sjBackTime=DateTime.Now,ygBackTime=DateTime.Now.AddMonths(+3)}
            };
            borrowItems.ForEach(s => context.borrowItems.AddOrUpdate(s));

            var dctionary = new List<Dictionary>(){
                new Dictionary{Id =1,Code="Novel",DisplayName="小说",Type="BookType"},
                new Dictionary{Id =2,Code="Magazine",DisplayName="杂志",Type="BookType"},
                new Dictionary{Id =3,Code="History",DisplayName="历史",Type="BookType"},
                new Dictionary{Id =4,Code="Medicine",DisplayName="医学",Type="BookType"},
                new Dictionary{Id =5,Code="Technology",DisplayName="科技",Type="BookType"},
            };
            dctionary.ForEach(s => context.Dictionarys.AddOrUpdate(s));

            context.SaveChanges();
        }
    }
}