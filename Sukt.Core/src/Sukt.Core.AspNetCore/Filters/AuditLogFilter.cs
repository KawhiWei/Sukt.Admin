using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktDependencyAppModule;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Entity;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using System.ComponentModel;
using Sukt.Core.Shared.Extensions;

namespace Sukt.Core.AspNetCore.Filters
{
    /// <summary>
    /// AuditLogFilter执行完成过滤器用来记录审计日志
    /// </summary>
    public class AuditLogFilter : IActionFilter, IResultFilter
    {
        /// <summary>
        /// 方法执行完成
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
        /// <summary>
        /// 方法执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
        /// <summary>
        /// 方法返回完成后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            IServiceProvider provider = context.HttpContext.RequestServices;
            var dic = provider.GetService<DictionaryAccessor>();
            dic.TryGetValue("audit", out object auditEntry);
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var actionname = action.MethodInfo.ToDescription();//获取控制器特性
            foreach (var item in (auditEntry as List<AuditEntry>))
            {
                item.Action = context.HttpContext.Request.Path;
                item.DescriptionName = actionname;
            }
            provider.GetService<IAuditStore>()?.SaveAudit((auditEntry as List<AuditEntry>)).GetAwaiter().GetResult(); //不用异步，或则用异步IResultFilterAsync
        }
        /// <summary>
        /// 方法返回中
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            
        }
    }
}
