using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// 持久化授权
    /// </summary>
    [DisplayName("持久化授权")]
    public class PersistedGrant : /*PersistedGrantBase*/EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public PersistedGrant(string key, string type, string subjectId, string sessionId, string clientId, string description, string data, DateTime? expiration, DateTime? consumedTime)
        {
            Key = key;
            Type = type;
            SubjectId = subjectId;
            SessionId = sessionId;
            ClientId = clientId;
            Description = description;
            Data = data;
            Expiration = expiration;
            ConsumedTime = consumedTime;
        }

        /// <summary>
        /// 键
        /// </summary>
        [DisplayName("键")]
        public string Key { get; private set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; private set; }
        /// <summary>
        /// 主题id
        /// </summary>
        [DisplayName("主题id")]
        public string SubjectId { get; private set; }
        /// <summary>
        /// 会话id
        /// </summary>
        [DisplayName("会话id")]
        public string SessionId { get; private set; }
        /// <summary>
        /// 客户端id
        /// </summary>
        [DisplayName("客户端id")]
        public string ClientId { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; private set; }
        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        public string Data { get; private set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTime? Expiration { get; private set; }
        /// <summary>
        /// 消费时间
        /// </summary>
        [DisplayName("消费时间")]
        public DateTime? ConsumedTime { get; private set; }
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
