using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Entity
{
    public class BaseQuery
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页行数
        /// </summary>
        public int PageRow { get; set; } = 10;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName { get; set; } = "Id";
        /// <summary>
        /// 排序方向
        /// </summary>
        public SortDirectionEnum SortDirection { get; set; } = SortDirectionEnum.Ascending;
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<FilterCondition> Filters { get; set; }
        /// <summary>
        /// 查询条件And或者Or
        /// </summary>
        public FilterConnect FilterConnect { get; set; } = FilterConnect.And;


    }
}
