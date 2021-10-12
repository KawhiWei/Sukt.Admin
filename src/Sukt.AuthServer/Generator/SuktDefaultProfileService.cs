using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public class SuktDefaultProfileService : ISuktProfileService
    {
        protected readonly ILogger logger;

        public SuktDefaultProfileService(ILogger logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 默认的自定义用户信息添加
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(SuktProfileDataRequestContext context)
        {
            logger.LogDebug("打印默认自定义用户上下文添加到claims");
            return Task.CompletedTask;
        }
    }
}
