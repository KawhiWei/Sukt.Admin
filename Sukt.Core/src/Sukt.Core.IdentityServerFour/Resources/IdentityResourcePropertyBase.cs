using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 身份资源属性
    /// </summary>
    [DisplayName("身份资源属性")]
    public abstract class IdentityResourcePropertyBase : Property
    {
        /// <summary>
        /// 身份资源id
        /// </summary>
        [DisplayName("身份资源id")]
        public Guid IdentityResourceId { get; set; }

        ///// <summary>
        ///// 身份资源
        ///// </summary>
        //[DisplayName("身份资源")]
        //public IdentityResourceBase IdentityResource { get; set; }
    }
}