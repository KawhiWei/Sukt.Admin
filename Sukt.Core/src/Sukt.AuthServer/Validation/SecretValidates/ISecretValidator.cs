using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.SecretValidates
{
    /// <summary>
    /// 密钥验证器服务接口
    /// </summary>
    public interface ISecretValidator
    {
        Task<SecretValidationResult> ValidateAsync(SuktApplicationModel suktApplication, ParsedSecret parsedSecret);
    }
}
