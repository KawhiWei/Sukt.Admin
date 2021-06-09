//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sukt.Core.EntityFrameworkCore
//{
//    public class EFContextFactory : IDesignTimeDbContextFactory<SuktContext>
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public EFContextFactory(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public SuktContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<SuktContext>();
//            optionsBuilder.UseMySql("", new MySqlServerVersion(new Version(8, 0, 23)));
//            return new SuktContext(optionsBuilder.Options, _serviceProvider);
//        }
//        private static IConfigurationRoot BuildConfiguration()
//        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Sukt.Core.API/"))
//                .AddJsonFile("appsettings.json", optional: false);
//            return builder.Build();
//        }
//    }
//}
