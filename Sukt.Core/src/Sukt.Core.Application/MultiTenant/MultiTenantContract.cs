using Sukt.Core.Domain.Models.MultiTenant;
using Sukt.Core.Dtos.MultiTenant;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;
using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.Extensions.ResultExtensions;

namespace Sukt.Core.Application.MultiTenant
{
    public class MultiTenantContract : IMultiTenantContract
    {
        private readonly IEFCoreRepository<MultiTenantEntity, Guid> _multiTenantRepository;

        public MultiTenantContract(IEFCoreRepository<MultiTenantEntity, Guid> multiTenantRepository)
        {
            _multiTenantRepository = multiTenantRepository;
        }

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
