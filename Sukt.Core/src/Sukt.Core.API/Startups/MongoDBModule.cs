using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.MongoDB;
using Sukt.Core.MongoDB.DbContexts;
using Sukt.Core.Shared.Extensions;

namespace Sukt.Core.API.Startups
{
    public class MongoDBModule : MongoDBModuleBase
    {
        protected override void AddDbContext(IServiceCollection services)
        {
            var connection = services.GetFileByConfiguration("SuktCore:MongoDBs:MongoDBConnectionString", "未找到存放MongoDB数据库链接的文件");
            services.AddMongoDbContext<DefaultMongoDbContext>(options =>
            {
                options.ConnectionString = connection;
            });
        }
    }
}
