using CSRedis;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Core.Caching;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktAppModules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sukt.Core.Redis
{
    public abstract class CSRedisModuleBase: SuktAppModuleBase
    {

        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            var redisPath = service.GetConfiguration()["SuktCore:Redis:ConnectionString"];
            var basePath = ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            var redisConn = Path.Combine(basePath, redisPath);
            if (!File.Exists(redisConn))
            {
                throw new Exception("未找到存放Rdis链接的文件");
            }
            var connStr = File.ReadAllText(redisConn).Trim();
            var csredis = new CSRedisClient(connStr);
            RedisHelper.Initialization(csredis);
            service.TryAddTransient(typeof(ICache<>), typeof(CSRedisCache<>));
            service.TryAddTransient(typeof(ICache<,>), typeof(CSRedisCache<,>));
            service.TryAddTransient<ICache, CSRedisCache>();
            return service;
        }
    }
}
