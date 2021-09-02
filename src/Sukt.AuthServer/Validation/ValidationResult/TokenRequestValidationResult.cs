using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    /// <summary>
    /// 获取token 请求验证返回实体
    /// </summary>
    public class TokenRequestValidationResult: ValidationResult
    {
        public TokenRequestValidationResult(ValidatedTokenRequest validatedRequest, Dictionary<string, object> customResponse=null)
        {
            IsError = false;
            ValidatedRequest = validatedRequest;
            CustomResponse = customResponse;
        }
        public TokenRequestValidationResult(ValidatedTokenRequest validatedRequest, string
             error , string errorDescription = null, Dictionary<string, object> customResponse=null)
        {
            IsError = true;
            Error = error;
            ErrorDescription = errorDescription;
            ValidatedRequest = validatedRequest;
            CustomResponse = customResponse;
        }
        /// <summary>
        /// 请求信息存储
        /// </summary>
        public ValidatedTokenRequest ValidatedRequest { get; }
        public Dictionary<string, object> CustomResponse { get; set; }
    }
}
