using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 客户端退出重定向uri
    /// </summary>
    [DisplayName("客户端退出重定向uri")]
    public abstract class ClientPostLogoutRedirectUriBase : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 客户端退出重定向uri
        /// </summary>
        [DisplayName("客户端退出重定向uri")]
        public string PostLogoutRedirectUri { get; set; }
    }
}