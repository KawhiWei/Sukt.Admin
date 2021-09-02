using Microsoft.Extensions.DependencyInjection;
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
            service.AddRedis("192.168.0.166:6379,password = redis123,defaultDatabase=5,prefix = test_");
        }
    }
}
