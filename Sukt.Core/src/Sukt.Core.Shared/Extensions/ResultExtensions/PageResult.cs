using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    public class PageResult<T>:ResultBase
    {
        public PageResult() : this(new T[0], 0, "查询成功", true)
        {

        }
        public PageResult(IEnumerable<T> data, int total, string message = "查询成功", bool success = true)
        {
            Data = data;
            Total = total;
            Success = success;
            this.Message = message;
        }


        public IEnumerable<T> Data { get; set; }

        public int Total { get; set; }
    }
}
