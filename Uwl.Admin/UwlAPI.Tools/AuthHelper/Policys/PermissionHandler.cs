using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Uwl.Data.Model.Result;
using Uwl.Data.Server.RoleAssigServices;
using Uwl.Extends.Utility;

namespace UwlAPI.Tools.AuthHelper.Policys
{
    /// <summary>
    /// PermissionHandler 自定义授权处理器，核心！
    /// 权限授权处理器 继承AuthorizationHandler ，并且需要一个权限必要参数
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider _Schemes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IRoleAssigServer _roleAssigServer { get; set; }
        private readonly IHttpContextAccessor _accessor;
        ///// <summary>
        ///// services 层注入
        ///// </summary>
        //public IRoleModulePermissionServices _roleModulePermissionServices { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationScheme"></param>
        /// <param name="roleAssigServer"></param>
        public PermissionHandler(IAuthenticationSchemeProvider authenticationScheme, IRoleAssigServer roleAssigServer, IHttpContextAccessor accessor)
        {
            this._Schemes = authenticationScheme;
            this._roleAssigServer = roleAssigServer;
            this._accessor = accessor;
        }
        /// <summary>
        /// 重写异步处理程序
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            //var filterContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext);
            //取出HttpContext上下文信息
            var httpContext = _accessor.HttpContext;
            //if(!requirement.Permissions.Any())
            //{
            //    _roleAssigServer.
            //}


            if (httpContext!=null)
            {
                var quesrUrl = httpContext.Request.Path.Value.ToLower();//获取请求的Url的Action
                //获取当前用户的角色信息
                var currentUserRoles = (from item in httpContext.User.Claims
                                        where item.Type == requirement.ClaimType
                                        select item.Value).ToArray();
                var guidarr = currentUserRoles.Select(x => x.ToGuid()).ToArray();
                var rolelist = await _roleAssigServer.GetRoleAction(guidarr);

                List<PermissionItem> permissions = new List<PermissionItem>();
                permissions.AddRange(rolelist.Select(x => new PermissionItem { Url = x.ActionName, Role = x.RoleName }));
                requirement.Permissions = permissions;                
                //判断请求是否停止
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await this._Schemes.GetRequestHandlerSchemesAsync())
                {
                    if(await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        //自定义异常返回数据
                        //httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //filterContext.Result = new JsonResult(model);
                        //context.Succeed(requirement);
                        context.Fail();
                        return;
                    }
                }
                //判断请求是否拥有凭据，即有没有登录
                var defaultAuthenticate = await _Schemes.GetDefaultAuthenticateSchemeAsync();
                if(defaultAuthenticate!=null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //判断是否登录    result?.Principal不为空即登录成功
                    if (result?.Principal!=null)
                    {
                        httpContext.User = result.Principal;
                        if(true)
                        {
                            var currentRoles = (from item in httpContext.User.Claims
                                                    where item.Type == requirement.ClaimType
                                                select item.Value.ToLower()).ToList();
                            var isMatchRole = false;
                            var permisssionRoles = requirement.Permissions.Where(x => currentRoles.Contains(x.Role.ToString().ToLower())).ToList();//根据当前用户角色获取所有的角色信息
                            foreach (var x in permisssionRoles)
                            {
                                try
                                {
                                    if(Regex.Match(quesrUrl,x.Url.ToLower()).Value==quesrUrl)
                                    {
                                        isMatchRole = true;//如果等于true 证明有权限
                                        break;
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            if(currentRoles.Count<=0|| !isMatchRole)
                            {
                                context.Fail();
                                return;
                            }
                        }
                        //判断过期时间（这里仅仅是最坏验证原则，你可以不要这个if else的判断，
                        //因为我们使用的官方验证，Token过期后上边的result?.Principal 就为 null 了，
                        //进不到这里了，因此这里其实可以不用验证过期时间，只是做最后严谨判断）
                        if ((httpContext.User.Claims.SingleOrDefault(s => s.Type == "exp")?.Value) != null 
                            && OverdueTimeToDateTime(httpContext.User.Claims.SingleOrDefault(s => s.Type == "exp")?.Value) >= DateTime.Now)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                        return;
                    }
                    else
                    {
                        context.Fail();
                        return;
                    }
                }
                if(quesrUrl.Equals(requirement.LoginPath.ToLower(),StringComparison.Ordinal)&& (!httpContext.Request.Method.Equals("POST")|| !httpContext.Request.HasFormContentType))
                {
                    context.Fail();
                    return;
                }
            }
            context.Succeed(requirement);
        }
        /// <summary>
        /// 转换时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime OverdueTimeToDateTime(string time)
        {

            time = time.Substring(0, 10);
            double timestamp = Convert.ToInt64(time);
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
            return dateTime;

        }
    }
}
