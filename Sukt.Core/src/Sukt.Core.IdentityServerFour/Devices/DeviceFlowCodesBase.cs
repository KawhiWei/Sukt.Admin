using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 设备代码
    /// </summary>
    [DisplayName("设备代码")]
    public abstract class DeviceFlowCodesBase : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 设备代码
        /// </summary>
        [DisplayName("设备代码")]
        public string DeviceCode { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        [DisplayName("用户代码")]
        public string UserCode { get; set; }

        /// <summary>
        /// 主题id
        /// </summary>
        [DisplayName("主题id")]
        public string SubjectId { get; set; }

        /// <summary>
        /// 会话id
        /// </summary>
        [DisplayName("会话id")]
        public string SessionId { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        [DisplayName("客户端id")]
        public string ClientId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 消费时间
        /// </summary>
        [DisplayName("消费时间")]
        public DateTime? ConsumedTime { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        public string Data { get; set; }
    }
}