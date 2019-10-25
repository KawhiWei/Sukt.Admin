using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Data.Model.MenuViewModel
{

    /// <summary>
    /// 角色分配权限实体模型
    /// </summary>
    public class RoleAssigMenuViewModel
    {
        public RoleAssigMenuViewModel()
        {
            children = new List<RoleAssigMenuViewModel>();
        }
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool expand { get; set; } = true;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 菜单下面的按钮列表
        /// </summary>
        public List<RoleAssigButtonViewModel> ButtonsList { get; set; } = new List<RoleAssigButtonViewModel>();
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool disabled { get; set; }
        /// <summary>
        /// 是否在权限角色表中存在
        /// </summary>
        public bool @checked { get; set; }
        /// <summary>
        /// 子级菜单
        /// </summary>
        public List<RoleAssigMenuViewModel> children { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
