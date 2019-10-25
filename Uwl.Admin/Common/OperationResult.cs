using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Utility.Common
{
    /// <summary>
    /// 定义一个公共的返回结果类
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 构造函数注入返回结果类型
        /// </summary>
        /// <param name="resultType"></param>
        public OperationResult(ResultType resultType)
        {
            Result = resultType;
        }
        /// <summary>
        /// 重载两个参数的构造函数
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="message"></param>
        public OperationResult(ResultType resultType,string message) : this(resultType)
        {
            Message = message;
        }
        /// <summary>
        /// 返回结果类型
        /// </summary>
        public ResultType Result { get; set; }
        /// <summary>
        /// 返回结果消息
        /// </summary>
        public string Message { get; set; }
    }
}
