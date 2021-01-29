using Sukt.Core.IdentityServerFour.Resources;
using Sukt.Core.Shared;
using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// api授权范围
    /// </summary>
    [DisplayName("api授权范围")]
    public abstract class ApiScopeBase : ResourceBase, IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
    
        /// <summary>
        /// 是否必须
        /// </summary>
        [Description("是否必须")]
        public bool Required { get; set; }

        /// <summary>
        /// 是否强调显示
        /// </summary>
        [Description("是否强调显示")]
        public bool Emphasize { get; set; }
    }
}