using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServer4Store.Entities
{
    /// <summary>
    /// 用户声明
    /// </summary>
    [DisplayName("用户声明")]
    public abstract class UserClaim : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; }
    }
}