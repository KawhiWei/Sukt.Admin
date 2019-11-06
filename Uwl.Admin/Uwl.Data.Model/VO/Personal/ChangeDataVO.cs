using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.VO.Personal
{
    public class ChangeDataVO
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 通过token获取到用户Id
        /// </summary>
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
