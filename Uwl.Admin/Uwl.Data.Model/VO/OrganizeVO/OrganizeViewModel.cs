using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.OrganizeVO
{
    public class OrganizeViewModel
    {
        public OrganizeViewModel()
        {
            children = new List<OrganizeViewModel>();
        }
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string title { get; set; }
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
        public OrganizeEnum OrganizeType { get; set; }
        /// <summary>
        /// 所有父级节点的ID
        /// </summary>
        public string ParentArr { get; set; }
        /// <summary>
        /// 组织架构状态
        /// </summary>
        public StateEnum OrganizeState { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool expand { get; set; } = true;
        /// <summary>
        /// 子级机构
        /// </summary>
        public List<OrganizeViewModel> children { get; set; }

    }
}
