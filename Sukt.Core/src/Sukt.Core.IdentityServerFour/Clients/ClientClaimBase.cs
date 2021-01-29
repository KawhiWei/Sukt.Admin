using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 客户端声明
    /// </summary>
    [DisplayName("客户端声明")]
    public abstract class ClientClaimBase : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [DisplayName("值")]
        public string Value { get; set; }
    }
}