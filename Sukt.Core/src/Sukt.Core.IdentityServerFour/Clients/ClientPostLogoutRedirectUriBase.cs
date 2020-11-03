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
        /// 客户端id
        /// </summary>
        [DisplayName("客户端id")]
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// 客户端退出重定向uri
        /// </summary>
        [DisplayName("客户端退出重定向uri")]
        public Guid ClientId { get; set; }

        ///// <summary>
        ///// 所属客户端
        ///// </summary>
        //[DisplayName("所属客户端")]
        //public Client Client { get; set; }
    }
}