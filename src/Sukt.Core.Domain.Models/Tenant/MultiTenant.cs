using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sukt.Core.Domain.Models.Tenant
{
    /// <summary>
    /// 租户管理
    /// </summary>
    [DisplayName("租户管理")]
    public class MultiTenant : AggregateRootBase<Guid>, IFullAuditedEntity<Guid>
    {
        public MultiTenant(string companyName, string linkMan, string phoneNumber, bool isEnable, string email)
        {
            CompanyName = companyName;
            LinkMan = linkMan;
            PhoneNumber = phoneNumber;
            IsEnable = isEnable;
            Email = email;
            TenantConntionStrings = new List<MultiTenantConnectionString>();
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
        /// 
        /// </summary>
        public List<MultiTenantConnectionString> TenantConntionStrings { get; private set; }
        /// <summary>
        /// 添加或修改租户连接字符串
        /// </summary>
        /// <param name="connectionStringId"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        public OperationResponse SetConnectionString(Guid connectionStringId, string name,string connectionString)
        {
            if(TenantConntionStrings.Any(x=>x.Name.Equals(name) && x.Id != connectionStringId))
            {
                return  OperationResponse.Error($"服务名称：【{name}】在租户【{this.CompanyName}】下已存在！");
            }
            var tenantConnectionString = TenantConntionStrings.FirstOrDefault(x => x.Id == connectionStringId);
            if(tenantConnectionString!=null)
            {
                tenantConnectionString.Update(name, connectionString);
            }
            else
            {
                TenantConntionStrings.Add(new MultiTenantConnectionString(this.Id,name,connectionString));
            }
            return OperationResponse.Ok();
        }
        public void RemoveConnectionString(Guid connectionStringId)
        {
            var tenantConnectionString = TenantConntionStrings.FirstOrDefault(x=>x.Id==connectionStringId);
            if (tenantConnectionString != null)
            {
                tenantConnectionString.Remove();
            }
        }
        public MultiTenantConnectionString GetConnectionString(Guid connectionStringId)
        {
            return TenantConntionStrings.FirstOrDefault(x => x.Id == connectionStringId);
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
