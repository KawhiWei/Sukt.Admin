using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.MultiTenancy
{
    public interface ITenantDbContext
    {
        /// <summary>
        /// 当前租户
        /// </summary>
        TenantInfo TenantInfo { get; }
    }
}
