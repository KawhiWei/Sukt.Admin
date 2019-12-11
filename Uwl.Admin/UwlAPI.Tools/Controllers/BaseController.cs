using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Uwl.Extends.Utility;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    [ApiController]
    public class BaseController<T> : Controller
    {
        /// <summary>
        /// 公共日志记录
        /// </summary>
        public  ILogger<T> _logger;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="logger"></param>
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 角色Id
        /// </summary>
        public static List<Guid> RoleIds { get; private set; }=new List<Guid>();

        /// <summary>
        /// 用户Id
        /// </summary>
        public static Guid? UserId { get; private set; } = null;

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
                    if(UserId==null)
                    {
                        UserId = httpContext.User.Claims.FirstOrDefault(item => item.Type == "Id").Value.ToGuid();
                    }
                }
            }
            base.OnActionExecuting(context);
        }

    }
}