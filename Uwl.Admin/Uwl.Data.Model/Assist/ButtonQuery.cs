using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.Assist
{
    /// <summary>
    /// 按钮查询实体
    /// </summary>
    public class ButtonQuery:BaseQuery
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 按钮状态
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
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
    }
}
