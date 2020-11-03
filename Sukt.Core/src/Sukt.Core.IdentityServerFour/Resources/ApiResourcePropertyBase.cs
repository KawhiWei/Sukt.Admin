using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// api资源属性
    /// </summary>
    [DisplayName("api资源属性")]
    public abstract class ApiResourcePropertyBase : Property
    {
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