using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// api资源属性
    /// </summary>
    [DisplayName("api资源属性")]
    public class ApiResourceProperty : /*ApiResourcePropertyBase*/EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public ApiResourceProperty(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 键
        /// </summary>
        [DisplayName("键")]
        public string Key { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        [DisplayName("值")]
        public string Value { get; private set; }
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
