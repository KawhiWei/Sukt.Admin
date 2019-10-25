using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 用户角色实体类
    /// </summary>
    [Serializable]
    public class SysUserRole : Entity
    {
        //用户或者部门ID
        public Guid UserIdOrDepId { get; set; }
        //角色ID
        public Guid RoleId { get; set; }
    }
}
