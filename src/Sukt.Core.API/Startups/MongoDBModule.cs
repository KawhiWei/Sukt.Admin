using Microsoft.Extensions.DependencyInjection;
using Sukt.MongoDB;
using Sukt.MongoDB.DbContexts;
using Sukt.Module.Core.Extensions;
using System.IO;

namespace Sukt.Core.API.Startups
{
    public class MongoDBModule : MongoDBModuleBase
    {
        protected override void AddDbContext(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var connection = services.GetConfiguration()["SuktCore:MongoDBs:MongoDBConnectionString"];
            //var connection = services.GetFileByConfiguration("SuktCore:DbContext:MongoDBConnectionString", "未找到存放MongoDB数据库链接的文件");
            if (Path.GetExtension(connection).ToLower() == ".txt") //txt文件
            {
                connection = provider.GetFileText(connection, $"未找到存放MongoDB数据库链接的文件");
            }
            services.AddMongoDbContext<DefaultMongoDbContext>(options =>
            {
                options.ConnectionString = connection;
            });
        }
    }
}
