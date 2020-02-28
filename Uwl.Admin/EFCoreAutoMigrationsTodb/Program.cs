using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Uwl.Data.EntityFramework.Uwl_DbContext;

namespace EFCoreAutoMigrationsTodb
{
    
    class Program
    {
        public Program()
        {

        }
        static void Main(string[] args)
        {
			AutoMigrationToSqlserver();


            //"dotnet ef migrations add InitialCreate";


			Console.WriteLine("Hello World!");
			Console.ReadLine();
        }
        static void AutoMigrationToSqlserver()
        {
            //IServiceProvider serviceProvider = new ServiceProvider();
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var dc = scope.ServiceProvider.GetService<DomainContext>();
            //    dc.Database.EnsureCreated();
            //}
        }

    }
}
