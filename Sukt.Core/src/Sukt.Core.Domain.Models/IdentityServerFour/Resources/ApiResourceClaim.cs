using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// api资源声明
    /// </summary>
    [DisplayName("api资源声明")]
    public class ApiResourceClaim : EntityBase<Guid> /*ApiResourceClaimBase*/, IFullAuditedEntity<Guid>
    {
        public ApiResourceClaim(string type)
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; private set; }
        /// <summary>
        /// api资源
        /// </summary>
        [Description("api资源")]
        public ApiResource ApiResource { get; private set; }
        #region 公共字段

        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreatedId { get; set; }

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

        #endregion 公共字段
    }
}
