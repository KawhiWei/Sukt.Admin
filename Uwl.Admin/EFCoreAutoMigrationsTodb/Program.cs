using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Uwl.Data.EntityFramework.Uwl_DbContext;

namespace EFCoreAutoMigrationsTodb
{
    class Program
    {
        static void Main(string[] args)
        {
			AutoMigrationToSqlserver();


            Console.WriteLine("Hello World!");
			Console.ReadLine();
        }
        static void AutoMigrationToSqlserver()
        {
   //         using (var db= new UwlDbContext())
   //         {
			//	//获取所有待迁移
			//	Console.WriteLine($"Pending Migrations：\n{string.Join('\n', db.Database.GetPendingMigrations().ToArray())}");

			//	Console.WriteLine("是否继续执行迁移?(Y/N)");

			//	if (Console.ReadLine().Trim().ToLower() == "n")
			//	{
			//		return;
			//	}

			//	Console.WriteLine("Migrating...");

			//	try
			//	{

			//		//执行迁移
			//		db.Database.Migrate();
			//	}
			//	catch (Exception e)
			//	{
			//		Console.WriteLine(e);
			//		throw;
			//	}
			//}
        }

    }
}
