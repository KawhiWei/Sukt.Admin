using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 角色权限实体类
    /// </summary>
    [Serializable]
    public class SysRoleRight:Entity
    {
        //角色Id
        public Guid RoleId { get; set; }
        //菜单Id
        public Guid MenuId { get; set; }
        //按钮Id
        public string ButtonIds { get; set; }
    }
}
