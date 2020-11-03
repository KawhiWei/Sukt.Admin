namespace Sukt.Core.Shared.Entity
{
    /// <summary>
    /// 多租户共用数据库字段接口
    /// </summary>
    /// <typeparam name="TenantKey"></typeparam>
    public interface ITenantEntity<TenantKey>
    {
        /// <summary>
        /// 创建人Ｉｄ
        /// </summary>
        TenantKey TenantId { get; set; }
    }
}