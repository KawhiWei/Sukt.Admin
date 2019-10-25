using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.Assist
{
    /// <summary>
    /// 菜单查询
    /// </summary>
    public class MenuQuery:BaseQuery
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单状态
        /// </summary>
        public StateEnum stateEnum { get; set; }
        /// <summary>
        /// JS事件
        /// </summary>
        public string JSKeyCode { get; set; }
        /// <summary>
        /// API接口
        /// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// 前端组件路由地址
        /// </summary>
        public string UrlAddress { get; set; }
    }
}
