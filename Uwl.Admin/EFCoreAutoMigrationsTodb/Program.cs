using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Uwl.Data.EntityFramework.Uwl_DbContext;

namespace EFCoreAutoMigrationsTodb
{
    
    class Program
    {
        private readonly IServiceProvider _serviceProvider;
        public Program(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
			Console.ReadLine();
        }
    }
    public class AutoAddMigrate
    {
        private readonly IServiceProvider _serviceProvider;
        public AutoAddMigrate(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Add()
        {
            var dc = _serviceProvider.GetService<UwlDbContext>();
            if (dc.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine(" Performing migration / 执行迁移中");
                dc.Database.Migrate();
                Console.WriteLine(" Migration completed  / 迁移完成");
            }
            else
            {
                Console.WriteLine(" No files found to migrate / 未找到需要迁移的文件");
            }
            Console.WriteLine(" press any key to exit");
            Console.ReadLine();
        }
    }
}
