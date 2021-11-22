using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Dtos.Tenant
{
    /// <summary>
    /// 租户字符串输入Dto
    /// </summary>
    public class MultiTenantConnectionStringInputDto
    {
        public Guid TenantId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
