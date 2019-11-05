using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.RoleAssigVO
{
    public class UpdateUserRoleVo
    {
        /// <summary>
        /// 选中的角色Id数组
        /// </summary>
        public string RoleIds { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid userId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateName { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreateId { get; set; }
    }
}
