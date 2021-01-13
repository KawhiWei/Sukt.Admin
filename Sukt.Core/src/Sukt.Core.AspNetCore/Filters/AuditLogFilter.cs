using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.OperationResult;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;

namespace Sukt.Core.AspNetCore.Filters
{
    /// <summary>
    /// AuditLogFilter执行完成过滤器用来记录审计日志
    /// </summary>
    public class AuditLogFilter : IActionFilter, IResultFilter
    {
        /// <summary>
        /// 执行行动时
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult result)
            {
                if (result.Value is AjaxResult ajax)
                {
                    var type = ajax.Type;
                    IServiceProvider provider = context.HttpContext.RequestServices;

                    AuditEntryDictionaryScoped dict = provider.GetService<AuditEntryDictionaryScoped>();
                    if (!ajax.Success)
                    {
                        dict.AuditChange.Message = ajax.Message;
                    }

                    dict.AuditChange.ResultType = type;
                }
            }
        }
        /// <summary>
        /// 方法执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            var controllerAction = context.ActionDescriptor as ControllerActionDescriptor;
            var isAuditEnabled = serviceProvider.GetAppSettings().AuditEnabled;
            if (isAuditEnabled)
            {
                AuditEntryDictionaryScoped auditEntryDictionaryScoped = serviceProvider.GetService<AuditEntryDictionaryScoped>();
                AuditChangeInputDto auditChange = new AuditChangeInputDto();
                auditChange.BrowserInformation = context.HttpContext.Request.Headers["User-Agent"].ToString();
                auditChange.Ip = context.HttpContext.GetClientIP();
                auditChange.FunctionName = $"{context.Controller.GetType().ToDescription()}-{controllerAction.MethodInfo.ToDescription()}";
                auditChange.Action = context.HttpContext.Request.Path;
                auditChange.StartTime = DateTime.Now;
                auditEntryDictionaryScoped.AuditChange = auditChange;
            }
        }
        /// <summary>
        /// 方法返回完成后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var isAuditEnabled = serviceProvider.GetAppSettings().AuditEnabled;
            if (isAuditEnabled)
            {

                var dic = serviceProvider.GetService<AuditEntryDictionaryScoped>();
                dic.AuditChange.ExecutionDuration = DateTime.Now.Subtract(dic.AuditChange.StartTime).TotalMilliseconds;
                serviceProvider.GetService<IAuditStore>()?.SaveAudit(dic.AuditChange).GetAwaiter().GetResult();
            }
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
