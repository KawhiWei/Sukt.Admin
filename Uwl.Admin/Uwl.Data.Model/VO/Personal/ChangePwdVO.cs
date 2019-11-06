using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.VO.Personal
{
    /// <summary>
    /// 修改密码实体模型
    /// </summary>
    public class ChangePwdVO
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string oldPassWord { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string newPassWord { get; set; }
        /// <summary>
        /// 判断两次密码是否一致
        /// </summary>
        public string passwdCheck { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid? UserId { get; set; } = Guid.Empty;
    }
}
