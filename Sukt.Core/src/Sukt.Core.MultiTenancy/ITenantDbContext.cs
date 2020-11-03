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