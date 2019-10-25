using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UwlAPI.Tools.Models.RoleAssigModel
{
    public class RoleAssigPermission
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 按钮ID
        /// </summary>
        public Guid ButtonId { get; set; }


    }
}
