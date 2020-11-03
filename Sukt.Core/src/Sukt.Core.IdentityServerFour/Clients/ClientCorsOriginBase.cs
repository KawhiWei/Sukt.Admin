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
        public Guid Id { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        [DisplayName("域名")]
        public string Origin { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        [DisplayName("客户端id")]
        public Guid ClientId { get; set; }

        ///// <summary>
        ///// 所属客户端
        ///// </summary>
        //[DisplayName("所属客户端")]
        //public Client Client { get; set; }
    }
}