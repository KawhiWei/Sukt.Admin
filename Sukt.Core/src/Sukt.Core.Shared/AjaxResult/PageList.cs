using Sukt.Core.Shared.Extensions.ResultExtensions;
using System.Collections.Generic;

namespace Sukt.Core.Shared.AjaxResult
{
    public class PageList<T> : ResultBase
    {
        public PageList() : this(new T[0], 0, "查询成功", true)
        {
        }

        public PageList(IEnumerable<T> data, int total, string message = "查询成功", bool success = true)
        {
            Data = data;
            Total = total;
            Success = success;
            this.Message = message;
        }

        /// <summary>
        /// 分页数据返回集合
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 查询条件的总条数
        /// </summary>
        public int Total { get; set; }
    }
}