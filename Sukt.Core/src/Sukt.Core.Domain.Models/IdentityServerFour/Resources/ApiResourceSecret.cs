using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// api资源
    /// </summary>
    [DisplayName("api资源")]
    public class ApiResourceSecret : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public ApiResourceSecret(string value, string type, DateTime? expiration)
        {
            Value = value;
            Type = type;
            Expiration = expiration;
            Created = DateTime.Now;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; private set; }
        /// <summary>
        /// 值
        /// </summary>
        [DisplayName("值")]
        public string Value { get; private set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime Created { get; private set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTime? Expiration { get; private set; }
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
