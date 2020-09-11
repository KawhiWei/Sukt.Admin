using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Sukt.Core.Identity;
using Sukt.Core.Shared.Entity;

namespace Sukt.Core.Domain.Models
{
    /// <summary>
    /// 用户登录
    /// </summary>
    [DisplayName("用户登录")]
    public class UserLoginEntity : UserLoginBase<Guid>, IFullAuditedEntity<Guid>
    {
        #region 公共字段
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid? CreatedId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreatedAt { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }
        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public virtual DateTime LastModifedAt { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        #endregion
    }
}
