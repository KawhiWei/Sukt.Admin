using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.MongoDB;
using Sukt.Core.MongoDB.DbContexts;
using Sukt.Core.Shared.Extensions;

namespace Sukt.Core.AuthenticationCenter.Startups
{
    public class MongoDBModelule : MongoDBModuleBase
    {
        protected override void AddDbContext(IServiceCollection services)
        {
            var connection = services.GetConfiguration()["SuktCore:DbContext:MongoDBConnectionString"];
            services.AddMongoDbContext<DefaultMongoDbContext>(options =>
            {
                options.ConnectionString = connection;
            });
        }
    }
}
