using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 属性
    /// </summary>
    public abstract class Property : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        [DisplayName("键")]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [DisplayName("值")]
        public string Value { get; set; }
    }

    /// <summary>
    /// 客户端属性
    /// </summary>
    [DisplayName("客户端属性")]
    public abstract class ClientPropertyBase : Property
    {
    }
}