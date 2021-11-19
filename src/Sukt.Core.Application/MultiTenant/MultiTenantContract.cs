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
using Sukt.Module.Core.ResultMessageConst;

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
            MultiTenantEntity entity = new(input.CompanyName, input.LinkMan, input.PhoneNumber, input.IsEnable, input.Email);
            return await _multiTenantRepository.InsertAsync(entity);
        }

        public async Task<IPageResult<MultiTenantOutPutPageDto>> GetPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            OrderCondition<MultiTenantEntity>[] orderConditions = new OrderCondition<MultiTenantEntity>[] { new OrderCondition<MultiTenantEntity>(o => o.CreatedAt, SortDirectionEnum.Descending) };
            request.OrderConditions = orderConditions;
            return await _multiTenantRepository.NoTrackEntities.ToPageAsync<MultiTenantEntity, MultiTenantOutPutPageDto>(request);
        }

        public async Task<OperationResponse> LoadFormAsync(Guid id)
        {
            MultiTenantEntity entity = await _multiTenantRepository.GetByIdAsync(id);
            return new OperationResponse(ResultMessage.AllocationSucces, entity.MapTo<MultiTenantOutPutPageDto>(), OperationEnumType.Success);

        }

        public async Task<OperationResponse> UpdateAsync(Guid id, MultiTenantInputDto input)
        {
            input.NotNull(nameof(input));
            MultiTenantEntity entity = await _multiTenantRepository.GetByIdAsync(id);
            entity.Update(input.CompanyName, input.LinkMan, input.PhoneNumber, input.IsEnable, input.Email);
            return await _multiTenantRepository.UpdateAsync(entity);
        }
        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            return await _multiTenantRepository.DeleteAsync(id);
        }
    }
}
