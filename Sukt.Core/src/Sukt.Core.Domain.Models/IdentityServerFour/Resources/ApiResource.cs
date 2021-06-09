using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// api资源
    /// </summary>
    [DisplayName("api资源")]
    public class ApiResource : AggregateRootBase<Guid>/*ApiResourceBase*/, IFullAuditedEntity<Guid>
    {
        public ApiResource(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        /// <summary>
        /// 添加用户声明
        /// </summary>
        /// <param name="userClaims"></param>
        public void AddUserClaims(List<string> userClaims)
        {
            if (UserClaims == null)
                UserClaims = new List<ApiResourceClaim>();
            UserClaims.AddRange(userClaims.Select(x => new ApiResourceClaim(x)));
        }
        public void AddSecrets(ApiResourceSecret apiResourceSecret)
        {
            if (Secrets == null)
                Secrets = new List<ApiResourceSecret>();
            Secrets.Add(apiResourceSecret);
        }
        /// <summary>
        /// 添加授权范围
        /// </summary>
        /// <param name="scopes"></param>
        public void AddScopes(List<string> scopes)
        {
            if (Scopes == null)
                Scopes = new List<ApiResourceScope>();
            Scopes.AddRange(scopes.Select(x => new ApiResourceScope(x)));
        }
        #region IdentityServer4 资源对象属性
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool Enabled { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; private set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        public string DisplayName { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; private set; }
        /// <summary>
        /// 是否显示在发现文档中
        /// </summary>
        [DisplayName("是否显示在发现文档中")]
        public bool ShowInDiscoveryDocument { get; set; }
        /// <summary>
        /// 允许的访问令牌登录算法
        /// </summary>
        [DisplayName("允许的访问令牌登录算法")]
        public string AllowedAccessTokenSigningAlgorithms { get; private set; }
        /// <summary>
        /// 是否不可编辑
        /// </summary>
        [DisplayName("是否不可编辑")]
        public bool NonEditable { get; private set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        [DisplayName("最后访问时间")]
        public DateTime? LastAccessed { get; private set; }
        #endregion
        #region 导航属性
        /// <summary>
        /// 密钥
        /// </summary>
        [DisplayName("密钥")]
        public List<ApiResourceSecret> Secrets { get; private set; }

        /// <summary>
        /// 授权范围
        /// </summary>
        [DisplayName("授权范围")]
        public List<ApiResourceScope> Scopes { get; private set; }

        /// <summary>
        /// 用户声明
        /// </summary>
        [DisplayName("用户声明")]
        public List<ApiResourceClaim> UserClaims { get; private set; }

        /// <summary>
        /// 属性
        /// </summary>
        [DisplayName("属性")]
        public List<ApiResourceProperty> Properties { get; private set; }
        #endregion

        #region 公共字段

        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public virtual DateTime LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        #endregion 公共字段
    }
}
