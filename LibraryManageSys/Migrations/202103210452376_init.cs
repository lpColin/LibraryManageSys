namespace LibraryManageSys.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_book",
                c => new
                    {
                        bookId = c.Int(nullable: false, identity: true),
                        bookName = c.String(nullable: false, maxLength: 2147483647),
                        author = c.String(maxLength: 2147483647),
                        publish = c.String(maxLength: 2147483647),
                        type = c.String(nullable: false, maxLength: 2147483647),
                        amount = c.Int(nullable: false),
                        currAmount = c.Int(nullable: false),
                        imageURL = c.String(maxLength: 2147483647),
                        introduction = c.String(maxLength: 2147483647),
                        addTime = c.DateTime(nullable: false),
                        addName = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.bookId);
            
            CreateTable(
                "dbo.tb_borrowItem",
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
                .ForeignKey("dbo.tb_book", t => t.bookId, cascadeDelete: true)
                .ForeignKey("dbo.tb_reader", t => t.readerId, cascadeDelete: true)
                .Index(t => t.bookId)
                .Index(t => t.readerId);
            
            CreateTable(
                "dbo.tb_reader",
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
                "dbo.tb_user",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        userName = c.String(nullable: false, maxLength: 2147483647),
                        password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_borrowItem", "readerId", "dbo.tb_reader");
            DropForeignKey("dbo.tb_borrowItem", "bookId", "dbo.tb_book");
            DropIndex("dbo.tb_borrowItem", new[] { "readerId" });
            DropIndex("dbo.tb_borrowItem", new[] { "bookId" });
            DropTable("dbo.tb_user");
            DropTable("dbo.tb_reader");
            DropTable("dbo.tb_borrowItem");
            DropTable("dbo.tb_book");
        }
    }
}
