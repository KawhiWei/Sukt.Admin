using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidatedTokenRequest: ValidatedRequest
    {
        /// <summary>
        /// 从请求中获取到的用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 认证类型
        /// </summary>
        public string GrantType { get; set; }
        /// <summary>
        /// 资源作用域
        /// </summary>
        public IEnumerable<string> RequestedScopes { get; set; }
    }
}
