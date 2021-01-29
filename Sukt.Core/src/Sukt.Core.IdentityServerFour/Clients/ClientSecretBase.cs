using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 密钥
    /// </summary>
    public abstract class Secret : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [DisplayName("值")]
        public string Value { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime Created { get; set; }
    }

    /// <summary>
    /// 客户端密钥
    /// </summary>
    [DisplayName("客户端密钥")]
    public abstract class ClientSecretBase : Secret
    {
    }
}