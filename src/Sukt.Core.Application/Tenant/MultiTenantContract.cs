using Sukt.Core.Domain.Models.Tenant;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;
using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.ResultMessageConst;
using Sukt.Module.Core;
using Sukt.Core.Dtos.Tenant;

namespace Sukt.Core.Application.Tenant
{
    public class MultiTenantContract : IMultiTenantContract
    {
        private readonly IAggregateRootRepository<MultiTenant, Guid> _multiTenantRepository;

        public MultiTenantContract(IAggregateRootRepository<MultiTenant, Guid> multiTenantRepository)
        {
            _multiTenantRepository = multiTenantRepository;
        }

        public async Task<OperationResponse> CreatAsync(MultiTenantInputDto input)
        {
            input.NotNull(nameof(input));
            MultiTenant entity = new(input.CompanyName, input.LinkMan, input.PhoneNumber, input.IsEnable, input.Email);
            return await _multiTenantRepository.InsertAsync(entity);
        }

        public async Task<IPageResult<MultiTenantOutPutPageDto>> GetPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            OrderCondition<MultiTenant>[] orderConditions = new OrderCondition<MultiTenant>[] { new OrderCondition<MultiTenant>(o => o.CreatedAt, SortDirectionEnum.Descending) };
            request.OrderConditions = orderConditions;
            return await _multiTenantRepository.NoTrackEntities.ToPageAsync<MultiTenant, MultiTenantOutPutPageDto>(request);
        }

        public async Task<OperationResponse> LoadFormAsync(Guid id)
        {
            MultiTenant entity = await _multiTenantRepository.GetByIdAsync(id);
            return new OperationResponse(ResultMessage.AllocationSucces, entity.MapTo<MultiTenantOutPutPageDto>(), OperationEnumType.Success);

        }

        public async Task<OperationResponse> UpdateAsync(Guid id, MultiTenantInputDto input)
        {
            input.NotNull(nameof(input));
            MultiTenant entity = await _multiTenantRepository.GetByIdAsync(id);
            entity.Update(input.CompanyName, input.LinkMan, input.PhoneNumber, input.IsEnable, input.Email);
            return await _multiTenantRepository.UpdateAsync(entity);
        }
        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            return await _multiTenantRepository.DeleteAsync(id);
        }
    }
}
