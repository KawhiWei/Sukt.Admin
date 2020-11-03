using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// api资源
    /// </summary>
    [DisplayName("api资源")]
    public abstract class ApiResourceSecretBase : Secret
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