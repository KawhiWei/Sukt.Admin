using CSRedis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Core.Caching;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;

namespace Sukt.Core.Redis
{
    public class CSRedisModuleBase : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            var connStr = service.GetFileByConfiguration("SuktCore:Redis:ConnectionString", "未找到存放Rdis链接的文件");
            var csredis = new CSRedisClient(connStr);
            RedisHelper.Initialization(csredis);
            service.TryAddSingleton(typeof(ICache<>), typeof(CSRedisCache<>));
            service.TryAddSingleton(typeof(ICache<,>), typeof(CSRedisCache<,>));
            service.TryAddSingleton<ICache, CSRedisCache>();
        }
    }
}
