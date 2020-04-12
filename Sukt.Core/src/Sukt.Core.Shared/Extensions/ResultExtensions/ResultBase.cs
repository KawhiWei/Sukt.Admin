using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    /// <summary>
    /// 返回前端数据基类模型
    /// </summary>
    public class ResultBase
    {
        public virtual bool Success { get; set; }


        public virtual string Message { get; set; }
    }
}
