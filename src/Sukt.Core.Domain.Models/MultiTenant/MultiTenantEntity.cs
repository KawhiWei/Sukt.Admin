using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.MultiTenant
{
    /// <summary>
    /// 租户管理
    /// </summary>
    [DisplayName("租户管理")]
    public class MultiTenantEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public MultiTenantEntity(string companyName, string linkMan, string phoneNumber, bool isEnable, string email)
        {
            CompanyName = companyName;
            LinkMan = linkMan;
            PhoneNumber = phoneNumber;
            IsEnable = isEnable;
            Email = email;
        }
        public void Update(string companyName, string linkMan, string phoneNumber, bool isEnable, string email)
        {
            CompanyName = companyName;
            LinkMan = linkMan;
            PhoneNumber = phoneNumber;
            IsEnable = isEnable;
            Email = email;
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        [DisplayName("公司名称")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        public string LinkMan { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool IsEnable { get; set; } = false;
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [DisplayName("邮箱地址")]
        public string Email { get; set; }
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
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        [DisplayName("最后修改人")]
        public Guid? LastModifyId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTimeOffset? LastModifedAt { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        #endregion
    }
}
