using MongoDB.Driver;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Extensions.OrderExtensions;

namespace Sukt.Core.Shared
{
    public static class FindFluentExtensions
    {
        public static IOrderedFindFluent<TEntity, TEntity> OrderBy<TEntity>(this IFindFluent<TEntity, TEntity> findFluent, OrderCondition[] orderConditions)
        {
            IOrderedFindFluent<TEntity, TEntity> orderFindFluent = null;
            if (orderConditions == null || orderConditions.Length == 0)
            {
                orderFindFluent = FindFluentSortBy<TEntity, TEntity>.OrderBy(findFluent, "Id", Enums.SortDirectionEnum.Ascending);
                //findFluent = FindFluentSortBy<TEntity, TEntity>.OrderBy(findFluent, "Id", Enums.SortDirectionEnum.Ascending);
            }
            orderConditions.ForEach((e, i) =>
            {
                orderFindFluent = i == 0 ? FindFluentSortBy<TEntity, TEntity>.OrderBy(findFluent, e.SortField, e.SortDirection) :
                FindFluentSortBy<TEntity, TEntity>.ThenBy(orderFindFluent, e.SortField, e.SortDirection);
            });
            return orderFindFluent;
        }
    }
}
