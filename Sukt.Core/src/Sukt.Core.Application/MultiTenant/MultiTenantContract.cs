using Sukt.Core.Domain.Models.MultiTenant;
using Sukt.Core.Dtos.MultiTenant;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Extensions.OrderExtensions;
using Sukt.Core.Shared.Extensions.ResultExtensions;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application.MultiTenant
{
    public class MultiTenantContract : IMultiTenantContract
    {
        private readonly IEFCoreRepository<MultiTenantEntity, Guid> _multiTenantRepository;
        public async Task<OperationResponse> CreatAsync(MultiTenantInputDto input)
        {
            input.NotNull(nameof(input));
            return await _multiTenantRepository.InsertAsync(input);
        }

        public async Task<IPageResult<MultiTenantOutPutPageDto>> GetLoadPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            OrderCondition<MultiTenantEntity>[] orderConditions = new OrderCondition<MultiTenantEntity>[] { new OrderCondition<MultiTenantEntity>(o => o.CreatedAt, SortDirectionEnum.Descending) };
            request.OrderConditions = orderConditions;
            return await _multiTenantRepository.NoTrackEntities.ToPageAsync<MultiTenantEntity, MultiTenantOutPutPageDto>(request);
        }

        public Task<OperationResponse> LoadAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResponse> UpdateAsync(MultiTenantInputDto input)
        {
            input.NotNull(nameof(input));
            return await _multiTenantRepository.UpdateAsync(input);
        }
    }
}
