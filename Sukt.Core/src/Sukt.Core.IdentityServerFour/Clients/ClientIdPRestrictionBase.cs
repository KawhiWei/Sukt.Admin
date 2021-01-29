using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 客户端限制提供器
    /// </summary>
    [DisplayName("客户端限制提供器")]
    public abstract class ClientIdPRestrictionBase : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 提供器
        /// </summary>
        public string Provider { get; set; }
    }
}