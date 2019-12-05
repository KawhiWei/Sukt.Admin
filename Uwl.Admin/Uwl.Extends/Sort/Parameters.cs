using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Extends.Sort
{
    public class Parameters
    {
        public Parameters() : this(1, 10)
        {

        }
        public Parameters(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            OrderConditions = new OrderCondition[] { };
        }
        /// <summary>
        /// 页码
        /// </summary>

        public int PageIndex { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序条件集合
        /// </summary>
        public OrderCondition[] OrderConditions { get; set; }
    }
}
