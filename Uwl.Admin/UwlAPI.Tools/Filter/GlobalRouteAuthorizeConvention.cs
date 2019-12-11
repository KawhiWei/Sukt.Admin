using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Uwl.Common.GlobalRoute;

namespace UwlAPI.Tools.Filter
{
    /// <summary>
    /// Summary:全局路由权限公约
    /// Remarks:目的是针对不同的路由，采用不同的授权过滤器
    /// 如果 controller 上不加 [Authorize] 特性，默认都是 Permission 策略
    /// 否则，如果想特例其他授权机制的话，需要在 controller 上带上  [Authorize]，然后再action上自定义授权即可，比如 [Authorize(Roles = "Admin")]
    /// </summary>
    public class GlobalRouteAuthorizeConvention : IApplicationModelConvention
    {
        /// <summary>
        /// 自动添加Authorize属性
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            //循环控制器
            foreach (var item in application.Controllers)
            {
                if(!item.Filters.Any(s=>s is AuthorizeFilter))
                {
                    // 没有写特性，就用全局的 Permission 授权
                    item.Filters.Add(new AuthorizeFilter(GlobalRouteAuthorizeVars.Name));
                }
            }
        }
    }
}
