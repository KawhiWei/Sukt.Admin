using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 角色组实体类
    /// </summary>
    [Serializable]
    public class SysRoleGroup:Entity
    {
        //角色组名称
        public string Name { get; set; }
        //备注
        public string Memo { get; set; }
        //存放多个角色的Id,或者用户Id
        public string RoleIds { get; set; }
    }
}
