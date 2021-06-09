using AutoMapper;
using Sukt.Core.Domain.Models.MultiTenant;
using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Dtos.MultiTenant
{
    [AutoMap(typeof(MultiTenantEntity))]
    public class MultiTenantOutPutPageDto : OutputDtoBase<Guid>
    {
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
    }
}
