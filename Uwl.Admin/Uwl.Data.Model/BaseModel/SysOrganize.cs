using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 组织架构实体对象
    /// </summary>
    public class SysOrganize : Entity
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { get; set; } = Guid.Empty;
        /// <summary>
        /// 组织深度（计算得出，
        /// 第一级默认0，以此类推，由子级得到所有的父级计算得出深度）
        /// </summary>
        public int Depth { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 组织架构类型
        /// </summary>
        public OrganizeEnum OrganizeType {get;set;}
        /// <summary>
        /// 所有父级节点的ID
        /// </summary>
        public string ParentArr { get; set; }
        /// <summary>
        /// 组织架构状态
        /// </summary>
        public StateEnum OrganizeState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength]
        public string Remaker { get; set; }
    }
}
