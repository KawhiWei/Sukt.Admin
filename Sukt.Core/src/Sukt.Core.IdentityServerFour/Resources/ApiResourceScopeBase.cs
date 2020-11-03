using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// Api资源范围
    /// </summary>
    [DisplayName("Api资源范围")]
    public abstract class ApiResourceScopeBase : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 授权范围
        /// </summary>
        [Description("授权范围")]
        public string Scope { get; set; }

        /// <summary>
        /// api资源id
        /// </summary>
        [Description("api资源id")]
        public Guid ApiResourceId { get; set; }

        ///// <summary>
        ///// api资源
        ///// </summary>
        //[Description("api资源")]
        //public ApiResourceBase ApiResource { get; set; }
    }
}