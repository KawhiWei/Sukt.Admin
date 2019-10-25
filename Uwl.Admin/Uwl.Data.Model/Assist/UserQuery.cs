using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.Assist
{
    public class UserQuery:BaseQuery
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public StateEnum stateEnum { get; set; } 
    }
}
