using AspectCore.DynamicProxy;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Aop.Aop
{
    /// <summary>
    /// 全局日志审计AOP代理器（如果需要使用请在appsetting.json中打开配置）
    /// </summary>
    public class LogAop: AbstractInterceptor
    {
        private DictionaryAccessor _dictionaryAccessor { get; set; }
        private IAuditStore _auditStore { get; set; }
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);
            _auditStore = context.ServiceProvider.GetService(typeof(IAuditStore)) as IAuditStore;
            _dictionaryAccessor = context.ServiceProvider.GetService(typeof(DictionaryAccessor)) as DictionaryAccessor;
            _dictionaryAccessor.TryGetValue("audit", out object auditEntry);
            Console.WriteLine($"{ auditEntry.ToString() }");
            Console.WriteLine("萨迪克缴纳税款觉得那肯定安定安康九十年代看看见你看看");
        }
    }
}
