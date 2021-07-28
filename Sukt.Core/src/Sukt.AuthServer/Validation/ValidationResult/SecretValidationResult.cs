using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    /// <summary>
    /// 密钥验证客户端返回类
    /// </summary>
    public class SecretValidationResult : ValidationResultBase
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 确认信息
        /// </summary>
        public string Confirmation { get; set; }
    }
}
