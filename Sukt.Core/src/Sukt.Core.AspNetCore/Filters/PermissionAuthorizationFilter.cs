using Microsoft.AspNetCore.Mvc.Filters;
using Sukt.Core.Shared.Permission;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Sukt.Core.AspNetCore.Filters
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorityVerification _authority;
        public PermissionAuthorizationFilter(IAuthorityVerification authority)
        {
            _authority = authority;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IServiceProvider provider = context.HttpContext.RequestServices;
            await _authority.IsPermission("");
        }
    }
}
