using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Models.Tenant
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("租户数据库连接字符串")]
    public class MultiTenantConnectionString : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public MultiTenantConnectionString()
        {
        }
        public MultiTenantConnectionString(Guid tenantId, string name, string value) : this()
        {
            //Id = SuktGuid.NewSuktGuid();
            Name = name;
            Value = value;
            TenantId = tenantId;
        }
        public void Update(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public void Remove()
        {
            this.IsDeleted = true;
        }
        //[DisplayName("租户Id")]
        //public MultiTenant Tenant { get; private set; }
        [DisplayName("服务名称")]

        public string Name { get; private set; }
        [DisplayName("连接字符串")]

        public string Value { get; private set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        [DisplayName("租户Id")]
        public Guid TenantId { get; private set; }

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
