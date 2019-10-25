using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.Assist
{
    public class RoleQuery:BaseQuery
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public StateEnum stateEnum { get; set; }
    }
}
