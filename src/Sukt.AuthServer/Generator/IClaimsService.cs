using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public interface IClaimsService
    {
        /// <summary>
        /// 获取生成AccessToken的Claims
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IEnumerable<Claim>> GetAccessTokenClaimsAsync(ClaimsPrincipal subject, /*ResourceValidationResult resources,*/ ValidatedRequest request);
    }
}
