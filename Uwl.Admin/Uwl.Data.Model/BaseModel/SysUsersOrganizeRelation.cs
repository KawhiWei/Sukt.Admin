using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 用户部门实体对象
    /// </summary>
    [Serializable]
    public class SysUsersOrganizeRelation:Entity<Guid>
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid OrgId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 是否主要岗位/部门
        /// </summary>
        public bool IsMain { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
