namespace LibraryManageSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        bookId = c.Int(nullable: false, identity: true),
                        bookName = c.String(nullable: false, maxLength: 2147483647),
                        author = c.String(maxLength: 2147483647),
                        publish = c.String(maxLength: 2147483647),
                        type = c.String(maxLength: 2147483647),
                        amount = c.Int(nullable: false),
                        currAmount = c.Int(nullable: false),
                        imageURL = c.String(maxLength: 2147483647),
                        introduction = c.String(maxLength: 2147483647),
                        addTime = c.DateTime(nullable: false),
                        addName = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.bookId);
            
            CreateTable(
                "dbo.BorrowItem",
                c => new
                    {
                        borrowId = c.Int(nullable: false, identity: true),
                        status = c.Int(nullable: false),
                        burrowTime = c.DateTime(nullable: false),
                        ygBackTime = c.DateTime(nullable: false),
                        sjBackTime = c.DateTime(nullable: false),
                        borrowOper = c.String(maxLength: 2147483647),
                        backOper = c.String(maxLength: 2147483647),
                        bookId = c.Int(nullable: false),
                        readerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.borrowId)
                .ForeignKey("dbo.Book", t => t.bookId, cascadeDelete: true)
                .ForeignKey("dbo.Reader", t => t.readerId, cascadeDelete: true)
                .Index(t => t.bookId)
                .Index(t => t.readerId);
            
            CreateTable(
                "dbo.Reader",
                c => new
                    {
                        readerId = c.Int(nullable: false, identity: true),
                        readerName = c.String(nullable: false, maxLength: 2147483647),
                        phoneNum = c.String(maxLength: 2147483647),
                        Gender = c.Int(nullable: false),
                        email = c.String(maxLength: 2147483647),
                        balance = c.Double(nullable: false),
                        enableBorrowNum = c.Int(nullable: false),
                        createName = c.String(nullable: false, maxLength: 2147483647),
                        createTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.readerId);
            
            CreateTable(
                "dbo.Dictionary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 50),
                        Code = c.String(maxLength: 50),
                        DisplayName = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 2147483647),
                        Password = c.String(nullable: false, maxLength: 20),
                        DisplayName = c.String(maxLength: 50),
                        EmailAddress = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowItem", "readerId", "dbo.Reader");
            DropForeignKey("dbo.BorrowItem", "bookId", "dbo.Book");
            DropIndex("dbo.BorrowItem", new[] { "readerId" });
            DropIndex("dbo.BorrowItem", new[] { "bookId" });
            DropTable("dbo.User");
            DropTable("dbo.Dictionary");
            DropTable("dbo.Reader");
            DropTable("dbo.BorrowItem");
            DropTable("dbo.Book");
        }
    }
}
