using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 客户端跨域配置
    /// </summary>
    [DisplayName("客户端跨域配置")]
    public abstract class ClientCorsOriginBase : IEntity<Guid>
    {
        public Guid Id { get; protected set; }
        /// <summary>
        /// 域名
        /// </summary>
        [DisplayName("域名")]
        public string Origin { get; protected set; }
    }
}