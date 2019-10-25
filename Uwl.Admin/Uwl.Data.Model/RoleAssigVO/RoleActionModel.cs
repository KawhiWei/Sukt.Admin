using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.RoleAssigVO
{
    /// <summary>
    /// 角色Action方法实体，供自定义策略授权使用
    /// </summary>
    public class RoleActionModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleName { get; set; }
        /// <summary>
        /// Action方法名称
        /// </summary>
        public string ActionName { get; set; }
    }
}
