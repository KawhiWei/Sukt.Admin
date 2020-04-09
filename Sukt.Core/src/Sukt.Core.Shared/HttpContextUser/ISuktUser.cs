using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sukt.Core.Shared.HttpContextUser
{
    public interface ISuktUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// ID属性
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsIdentity();
        /// <summary>
        /// 解析Claim
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        List<string> GetClaimValueByType(string ClaimType);
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        string GetToken();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        List<string> GetUserInfoFromToken(string ClaimType);
    }
}
