using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 持久化授权
    /// </summary>
    [DisplayName("持久化授权")]
    public abstract class PersistedGrantBase : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        [DisplayName("键")]
        public string Key { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; }

        /// <summary>
        /// 主题id
        /// </summary>
        [DisplayName("主题id")]
        public string SubjectId { get; set; }

        /// <summary>
        /// 会话id
        /// </summary>
        [DisplayName("会话id")]
        public string SessionId { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        [DisplayName("客户端id")]
        public string ClientId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 消费时间
        /// </summary>
        [DisplayName("消费时间")]
        public DateTime? ConsumedTime { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        public string Data { get; set; }
    }
}