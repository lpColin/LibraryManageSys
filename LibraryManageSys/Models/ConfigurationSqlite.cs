using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LibraryManageSys.Models
{
    internal sealed class ConfigurationSqlite : DbMigrationsConfiguration<LMSEntitys>
    {
        public ConfigurationSqlite()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        //  This method will be called after migrating to the latest version.
        //  You can use the DbSet<T>.Add() helper extension method
        //  to avoid creating duplicate seed data.
        protected override void Seed(LMSEntitys context)
        {
            //针对sql server添加字段注释
            //List<PropertyInfo[]> listperoperties = new List<PropertyInfo[]>()
            //{
            //    typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance),
            //};
            //GetDescription(listperoperties, context); 
        }

        //void GetDescription(List<PropertyInfo[]> listperoperties, SqliteDbContext context)
        //{
        //    for (int j = 0; j < listperoperties.Count; j++)
        //    {
        //        for (int i = 1; i < listperoperties[j].Length; i++)
        //        {
        //            object[] objs = listperoperties[j][i].GetCustomAttributes(typeof(DescriptionAttribute), true);
        //            if (objs.Length > 0)
        //            {
        //                context.Database.ExecuteSqlCommand("ALTER TABLE " + listperoperties[j][0].Name + " MODIFY COLUMN " + listperoperties[j][i].Name + " VARCHAR(30) NOT NULL COMMENT " + "'" + ((DescriptionAttribute)objs[0]).Description + "'");
        //            }
        //        }
        //    }
        //}

    }
}
