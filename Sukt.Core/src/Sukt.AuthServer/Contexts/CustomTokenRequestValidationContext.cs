using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Contexts
{
    /// <summary>
    /// 自定义Token令牌请求上下文
    /// </summary>
    public class CustomTokenRequestValidationContext
    {
        public TokenRequestValidationResult Result { get; set; }
    }
}
