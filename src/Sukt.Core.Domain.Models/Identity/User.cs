using Sukt.Core.Domain.Models.Identity.Enum;
using Sukt.Core.Domain.Models.Organization;
using Sukt.Core.Identity;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DisplayName("用户信息")]
    public class User : UserBase<Guid>, IFullAuditedEntity<Guid>, ITenantEntity<Guid>
    {
        public User(
            DateTime birthday, string education, string technicalLevel, string idCard, bool isEnable, string duties, 
            string department, UserTypeEnum userType,
            string userName, string normalizedUserName, string nickName, string email, string normalizeEmail,
            bool emailConfirmed, string passwordHash, string headImg, string securityStamp, string concurrencyStamp, 
            string phoneNumber, bool phoneNumberConfirmed, bool twoFactorEnabled, DateTimeOffset? lockoutEnd, 
            bool lockoutEnabled, int accessFailedCount, bool isSystem, string sex) : 
            base(userName, normalizedUserName, nickName, email, normalizeEmail, 
                emailConfirmed, passwordHash, headImg, securityStamp, concurrencyStamp, 
                phoneNumber, phoneNumberConfirmed, twoFactorEnabled, lockoutEnd, lockoutEnabled, 
                accessFailedCount, isSystem, sex)
        {
            Id= SuktGuid.NewSuktGuid();
            Birthday = birthday;
            Education = education;
            TechnicalLevel = technicalLevel;
            IdCard = idCard;
            IsEnable = isEnable;
            Duties = duties;
            Department = department;
            UserType = userType;
        }

        public void SetFunc(DateTime birthday, string education, string technicalLevel, string idCard, bool isEnable, string duties,
            string department, UserTypeEnum userType)
        {
            Birthday = birthday;
            Education = education;
            TechnicalLevel = technicalLevel;
            IdCard = idCard;
            IsEnable = isEnable;
            Duties = duties;
            Department = department;
            UserType = userType;
        }

        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public DateTime Birthday { get; private set; }

        /// <summary>
        /// 学历
        /// </summary>
        [DisplayName("学历")]
        public string Education { get; private set; }

        /// <summary>
        /// 专业技术等级
        /// </summary>
        [DisplayName("专业技术等级")]
        public string TechnicalLevel { get; private set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [DisplayName("身份证号")]
        public string IdCard { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool IsEnable { get; private set; }

        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        public string Duties { get; private set; }
        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        public string Department { get; private set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [DisplayName("用户类型")]
        public UserTypeEnum UserType { get; private set; }
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
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTimeOffset? LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        [DisplayName("租户")]
        public Guid TenantId { get; set; }
        #endregion 公共字段
    }
}
