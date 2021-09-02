using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    /// <summary>
    /// 验证信息返回基类
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// 是否异常
        /// </summary>
        public bool IsError { get; set; } = true;
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 异常信息描述
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}
