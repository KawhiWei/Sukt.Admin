using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Middleware
{
    /// <summary>
    /// 端点路由处理中间件
    /// </summary>
    public class SuktAuthServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public SuktAuthServerMiddleware(RequestDelegate next, ILogger<SuktAuthServerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context, IEndpointRouter router)
        {
            context.Response.OnStarting(async () =>
            {
                await Task.CompletedTask;
                //if (context.GetSignOutCalled())
                //{
                //    _logger.LogDebug("SignOutCalled set; processing post-signout session cleanup.");

                //    // this clears our session id cookie so JS clients can detect the user has signed out
                //    await session.RemoveSessionIdCookieAsync();

                //    // back channel logout
                //    var logoutContext = await session.GetLogoutNotificationContext();
                //    if (logoutContext != null)
                //    {
                //        await backChannelLogoutService.SendLogoutNotificationsAsync(logoutContext);
                //    }
                //}
            });
            try
            {
                //查找端点路由，路由就是path请求地址，根据请求地址寻找不同的处理方法
                var endpoint = router.FindHandler(context);
                if (endpoint != null)
                {
                    _logger.LogInformation("Invoking IdentityServer endpoint: {endpointType} for {url}", endpoint.GetType().FullName, context.Request.Path.ToString());
                    var result = await endpoint.HandlerProcessAsync(context);
                    if (result != null)
                    {
                        _logger.LogTrace("Invoking result: {type}", result.GetType().FullName);
                        await result.ExecuteAsync(context);
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                //await events.RaiseAsync(new UnhandledExceptionEvent(ex));
                _logger.LogError(ex, "未处理的异常！", ex.Message);
                throw;
            }
            await _next(context);


        }
    }
}
