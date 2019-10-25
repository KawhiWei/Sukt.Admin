using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Utility.Common
{
    public class PageDataView<T>
    {
        public PageDataView()
        {
            this.ItemsList = new List<T>();
        }
        public int TotalCount { get; set; }
        public IList<T> ItemsList { get; set; }
    }
    /// <summary>
    /// 提供分页的参数
    /// </summary>
    public class PageCriteria
    {
        public PageCriteria()
        {
            ParamsList = new List<ProcParamHelp>();
        }
        /// <summary>
        /// 表明
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 需要查询的字段
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey { get; set; }
        /// <summary>
        /// 查询的当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBySort { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Wherecondition { get; set; }
        /// <summary>
        /// 查询的到总数量
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 查询需要的参数
        /// </summary>
        public List<ProcParamHelp> ParamsList { get; set; }
    }
    public class ProcParamHelp
    {
        /// <summary>
        /// 参数名称【必须带有@】
        /// </summary>
        public string ParamName { get; set; } = string.Empty;
        /// <summary>
        /// 参数值
        /// </summary>
        public object ParamValue { get; set; } = string.Empty;
        /// <summary>
        /// 参数完整类型【字符类型必须指定大小】 例如 int varchar(10) nvarchar(10) uniqueidentifier bit 等
        /// </summary>
        public string ParamType { get; set; } = string.Empty;
    }
}
