using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Extensions;
using Sukt.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.API.Startups
{
    public class RedisModule: RedisModuleBase
    {
        public override void AddRedis(IServiceCollection service)
        {
            IConfiguration configuration = service.GetConfiguration();
            string connectionstr = configuration["SuktCore:Redis:ConnectionString"];
            service.AddRedis(connectionstr);
        }
    }
}
