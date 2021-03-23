using LibraryManageSys.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LibraryManageSys.Models
{
    /// <summary>
    /// define data table
    /// </summary>
    public class LMSEntitys:DbContext
    {
        public LMSEntitys() : base("sqliteDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LMSEntitys, LMSInitializer>());
            this.Configuration.LazyLoadingEnabled = false; //关闭延迟加载
        }
        public DbSet<User> users { get; set; }

        public DbSet<Reader> readers { get; set; }

        public DbSet<Book> books { get; set; }

        public DbSet<BorrowItem> borrowItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new SqliteDropCreateDatabaseWhenModelChanges<SqliteDbContext>(modelBuilder));
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //去除复数表名称
            modelBuilder.Configurations.AddFromAssembly(typeof(LMSEntitys).Assembly);//dynamically load all configuration
        }
    }
}