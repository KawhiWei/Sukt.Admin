using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Core.Caching;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;
using System.IO;

namespace Sukt.Core.Redis
{
    public class CSRedisModuleBase : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            var provider = service.BuildServiceProvider();
            var connection = service.GetConfiguration()["SuktCore:Redis:ConnectionString"]; //service.GetFileByConfiguration("SuktCore:Redis:ConnectionString", "未找到存放Rdis链接的文件");
            if (Path.GetExtension(connection).ToLower() == ".txt") //txt文件
            {
                connection = provider.GetFileText(connection, $"未找到存放MongoDB数据库链接的文件");
            }
            var csredis = new CSRedisClient(connection);
            RedisHelper.Initialization(csredis);
            service.TryAddSingleton(typeof(ICache<>), typeof(CSRedisCache<>));
            service.TryAddSingleton(typeof(ICache<,>), typeof(CSRedisCache<,>));
            service.TryAddSingleton<ICache, CSRedisCache>();
        }
    }
}
