using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Uwl.Common.HttpContextUser
{
    public interface IUsers
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string Name { get; }
        Guid Id { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);

        string GetToken();
        List<string> GetUserInfoFromToken(string ClaimType);
    }
}
