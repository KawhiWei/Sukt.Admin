using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    /// <summary>
    /// 验证基类返回基类
    /// </summary>
    public class ValidationResultBase
    {
        /// <summary>
        /// 是否错误
        /// </summary>
        public bool IsError { get; set; } = true;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 错误信息描述
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}
