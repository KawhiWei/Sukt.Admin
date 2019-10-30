using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.CacheModel
{
    public class SignalRModel
    {
        /// <summary>
        /// 用户每次登陆的链接ID
        /// </summary>
        public string SignalRConnectionId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public Guid DepId { get; set; }
    }
}
