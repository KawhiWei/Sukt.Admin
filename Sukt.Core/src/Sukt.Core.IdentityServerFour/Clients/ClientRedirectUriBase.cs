using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 客户端允许的重定向uri
    /// </summary>
    [DisplayName("客户端允许的重定向uri")]
    public abstract class ClientRedirectUriBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 重定向uri
        /// </summary>
        [DisplayName("重定向uri")]
        public string RedirectUri { get; protected set; }
    }
}