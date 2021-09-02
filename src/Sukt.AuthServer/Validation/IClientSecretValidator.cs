using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    public interface IClientSecretValidator
    {
        /// <summary>
        /// 客户端请求验证器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<ClientSecretValidationResult> ValidateAsync(HttpContext context);
    }
}
