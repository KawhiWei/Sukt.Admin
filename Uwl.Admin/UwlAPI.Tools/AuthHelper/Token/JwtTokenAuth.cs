using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UwlAPI.Tools.AuthHelper.Token
{
    /// <summary>
    /// 定义执行执行中间件类
    /// </summary>
    public class JwtTokenAuth
    {
        /// <summary>
        /// 定义委托
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 注入一个委托
        /// </summary>
        /// <param name="next"></param>
        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 中间件执行方法判断是否存在
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public  Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/Login/CustomGetToken")
            {
                 return _next(httpContext);
            }
            else
            {
                //检测是否包含'Authorization'请求头
                if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                {


                    return _next(httpContext);
                }
                //解析Authorization的令牌
                var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (tokenHeader.Length >= 128)
                {
                    TokenModelJWT token = JwtHelper.SerializeJwt(tokenHeader);
                    //获取角色授权
                    var claimList = new List<Claim>();
                    var claim = new Claim(ClaimTypes.Role, token.Role);
                    claimList.Add(claim);
                    var identity = new ClaimsIdentity(claimList);
                    var principal = new ClaimsPrincipal(identity);
                    httpContext.User = principal;
                }
                return _next(httpContext);
            }
    
        }
    }
    
}
