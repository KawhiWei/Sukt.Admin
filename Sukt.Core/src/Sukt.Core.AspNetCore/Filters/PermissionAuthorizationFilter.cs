using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Sukt.Core.AspNetCore.ApiBase;
using Sukt.Core.Shared.OperationResult;
using Sukt.Core.Shared.Permission;
using Sukt.Core.Shared.ResultMessageConst;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sukt.Core.AspNetCore.Filters
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorityVerification _authority;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionAuthorizationFilter(IAuthorityVerification authority, IHttpContextAccessor httpContextAccessor)
        {
            _authority = authority;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await Task.CompletedTask;
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var isAllowAnonymous = action.ControllerTypeInfo.GetCustomAttribute<AllowAnonymousAttribute>();//获取Action中的特性
            var linkurl = context.HttpContext.Request.Path.Value.Replace("/api/", "");
            var result = new AjaxResult(ResultMessage.Unauthorized, Shared.Enums.AjaxResultType.Unauthorized);
            if (!action.EndpointMetadata.Any(x => x is AllowAnonymousAttribute) && action.ControllerTypeInfo.GetType().IsAssignableFrom(typeof(ApiControllerBase)))
            {
                if (!(bool)_httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Result = new JsonResult(result);
                    return;
                }
                //if (!await _authority.IsPermission(linkurl.ToLower()))
                //{
                //    ////????不包含的时候怎么返回出去？这个请求终止掉
                //    ///
                //    result.Message = ResultMessage.Uncertified;
                //    context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                //    context.Result = new JsonResult(result);
                //    return;
                //}
            }
        }
    }
}
