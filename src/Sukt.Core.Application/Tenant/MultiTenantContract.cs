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
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sukt.Core.Application.Tenant
{
    public class MultiTenantContract : IMultiTenantContract
    {
        private readonly IAggregateRootRepository<MultiTenant, Guid> _multiTenantRepository;

        public MultiTenantContract(IAggregateRootRepository<MultiTenant, Guid> multiTenantRepository)
        {
            _multiTenantRepository = multiTenantRepository;
        }
        #region 租户管理
        public async Task<OperationResponse> CreateAsync(MultiTenantInputDto input)
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
        #endregion

        #region 租户连接字符串操作
        public async Task<OperationResponse> CreateAsync(Guid tenantId, MultiTenantConnectionStringInputDto input)
        {
            MultiTenant entity = await _multiTenantRepository.NoTrackEntities.Where(x => x.Id == tenantId).Include(x => x.TenantConntionStrings).SingleOrDefaultAsync();
            if (entity == null)
            {
                return new OperationResponse("未找到该租户", OperationEnumType.Error);
            }
            var result = entity.SetConnectionString(Guid.Empty, input.Name, input.Value);
            if (!result.Success)
            {
                return result;
            }
            return await _multiTenantRepository.UpdateAsync(entity);
        }
        public async Task<OperationResponse> UpdateAsync(Guid tenantId, Guid id, MultiTenantConnectionStringInputDto input)
        {
            MultiTenant entity = await _multiTenantRepository.NoTrackEntities.Where(x => x.Id == tenantId).Include(x => x.TenantConntionStrings).SingleOrDefaultAsync();
            if (entity == null)
            {
                return new OperationResponse("未找到该租户", OperationEnumType.Error);
            }
            var result = entity.SetConnectionString(id, input.Name, input.Value);
            if (!result.Success)
            {
                return result;
            }
            return await _multiTenantRepository.UpdateAsync(entity);
        }
        public async Task<OperationResponse> DeleteAsync(Guid tenantId, Guid id)
        {
            MultiTenant entity = await _multiTenantRepository.NoTrackEntities.Where(x => x.Id == tenantId).Include(x => x.TenantConntionStrings).SingleOrDefaultAsync();
            if (entity == null)
            {
                return new OperationResponse("未找到该租户", OperationEnumType.Error);
            }
            entity.RemoveConnectionString(id);
            return await _multiTenantRepository.UpdateAsync(entity);
        }
        public async Task<OperationResponse> LoadFormAsync(Guid tenantId, Guid id)
        {
            MultiTenant entity = await _multiTenantRepository.NoTrackEntities.Where(x => x.Id == tenantId).Include(x => x.TenantConntionStrings).SingleOrDefaultAsync();
            if (entity == null)
            {
                return new OperationResponse("未找到该租户", OperationEnumType.Error);
            }
            return OperationResponse.Ok(ResultMessage.DataSuccess, entity.GetConnectionString(id).MapTo<MultiTenantConnectionStringOutPutDto>());
        }
        public async Task<IPageResult<MultiTenantConnectionStringOutPutDto>> GetPageAsync(Guid tenantId, PageRequest request)
        {
            request.NotNull(nameof(request));
            OrderCondition<MultiTenantConnectionString>[] orderConditions = new OrderCondition<MultiTenantConnectionString>[] { new OrderCondition<MultiTenantConnectionString>(o => o.CreatedAt, SortDirectionEnum.Descending) };
            request.OrderConditions = orderConditions;
            return await _multiTenantRepository.NoTrackEntities.Where(x => x.Id == tenantId).Include(x => x.TenantConntionStrings).SelectMany(x => x.TenantConntionStrings).ToPageAsync<MultiTenantConnectionString, MultiTenantConnectionStringOutPutDto>(request);
        }
        #endregion
    }
}
