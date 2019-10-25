using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 菜单实体类
    /// </summary>
    [Serializable]
    public class SysMenu : Entity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// //备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// //图标名称
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// //父级ID
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 前端路由地址
        /// </summary>
        public string UrlAddress { get; set; }
        ///// <summary>
        ///// API数据请求地址
        ///// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 该菜单的所有父级
        /// </summary>
        public string ParentIdArr { get; set; }

    }
}
