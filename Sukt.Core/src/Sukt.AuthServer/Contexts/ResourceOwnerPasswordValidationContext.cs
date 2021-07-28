using Sukt.AuthServer.Domain.Enums;
using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Contexts
{
    /// <summary>
    /// 密码验证模式上下文
    /// </summary>
    public class ResourceOwnerPasswordValidationContext
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 请求上下文
        /// </summary>
        public ValidatedTokenRequest Request { get; set; }
        /// <summary>
        /// 认证返回消息体
        /// </summary>
        public GrantValidationResult Result { get; set; } = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
    }
}
