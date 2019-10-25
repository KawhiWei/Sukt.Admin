using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Uwl.Extends.Utility;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    [ApiController]
    public class BaseController : Controller
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public List<Guid> RoleIds { get; private set; }=new List<Guid>();

        /// <summary>
        /// action执行前事件
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext=HttpContext;
            if(httpContext.User.Claims.Any())
            {
                if (httpContext != null)
                {
                    if (!RoleIds.Any())
                    {
                        RoleIds = httpContext.User.Claims.Where(item => item.Type == ClaimTypes.Role).FirstOrDefault().Value.Split(',').Select(x => x.ToGuid()).ToList();
                    }
                }
            }
            base.OnActionExecuting(context);
        }

    }
}