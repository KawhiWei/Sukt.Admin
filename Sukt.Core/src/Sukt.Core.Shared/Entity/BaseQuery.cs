using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Extensions.OrderExtensions;
using Sukt.Core.Shared.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Entity
{
    public class PageRequest:PageParameters
    {
        public PageRequest()
        {
            PageIndex = 1;
            PageRow = 10;
            OrderConditions = new OrderCondition[] { };
            queryFilter = new QueryFilter();
        }
    }


    public class PageParameters : IFilteredPagedRequest, IPagedRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get ; set ; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PageRow { get ; set ; }
        /// <summary>
        /// 排序集合
        /// </summary>
        public OrderCondition[] OrderConditions { get ; set ; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public QueryFilter queryFilter { get ; set ; }
    }
    /// <summary>
    /// 查询条件接口
    /// </summary>
    public interface IFilteredPagedRequest: IPagedRequest
    {
        QueryFilter queryFilter { get; set; }
    }
    /// <summary>
    /// 分页所需的参数
    /// </summary>
    public interface IPagedRequest
    {
        public OrderCondition[] OrderConditions { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 页行数
        /// </summary>
        int PageRow { get; set; }
    }
}
