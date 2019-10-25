using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    [Serializable]
    public class SysRole:Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 角色状态
        /// </summary>
        public StateEnum RoletState { get; set; } = StateEnum.Normal;
    }
}
