using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.MenuViewModel
{
    /// <summary>
    /// 树形按钮实体类
    /// </summary>
    public class RoleAssigButtonViewModel
    {
        /// <summary>
        /// 按钮主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string lable { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool @checked { get; set; } = false;
    }
}
