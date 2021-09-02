using AutoMapper;
using Sukt.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Models
{
    /// <summary>
    /// 客户端验证模型
    /// </summary>
    public class SuktApplicationModel
    {
        /// <summary>
        /// 客户端唯一Id
        /// </summary>
        [DisplayName("客户端唯一Id")]
        public string ClientId { get; set; }
        /// <summary>
        /// 客户端密钥
        /// </summary>
        [DisplayName("客户端密钥")]
        public string ClientSecret { get; set; }
        /// <summary>
        /// 密钥类型
        /// </summary>
        [DisplayName("密钥类型")]
        public string SecretType { get; private set; }
        /// <summary>
        /// 客户端显示名称
        /// </summary>
        [DisplayName("客户端显示名称")]
        public string ClientName { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        [DisplayName("客户端类型")]
        public string ClientGrantType { get; set; }
        /// <summary>
        /// 退出登录回调地址
        /// </summary>
        [DisplayName("退出登录回调地址")]
        public ICollection<string> PostLogoutRedirectUris { get; set; }
        /// <summary>
        /// 登录重定向地址
        /// </summary>
        [DisplayName("登录重定向地址")]
        public ICollection<string> RedirectUris { get; set; }
        /// <summary>
        /// 属性配置
        /// </summary>
        [DisplayName("属性配置")]
        public string Properties { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }
        /// <summary>
        /// 客户端访问作用域
        /// </summary>
        [DisplayName("客户端访问作用域")]
        public ICollection<string> ClientScopes { get; set; }
        /// <summary>
        /// 协议类型
        /// </summary>
        [DisplayName("协议类型")]
        public string ProtocolType { get; set; }
    }
}
