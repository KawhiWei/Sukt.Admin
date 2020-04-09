using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.AOP
{
    public interface IAopManager
    {
        /// <summary>
        /// 自动注入AOP
        /// </summary>
        /// <param name="services"></param>
        void AutoLoadAops(IServiceCollection services);
    }
}
