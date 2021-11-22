using Sukt.Core.Dtos.Tenant;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Tenant
{
    public class MultiTenantConntionStringContract : IMultiTenantConntionStringContract
    {
        public Task<OperationResponse> CreatAsync(MultiTenantConntionStringInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IPageResult<MultiTenantOutPutPageDto>> GetPageAsync(PageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse> LoadFormAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse> UpdateAsync(Guid id, MultiTenantConntionStringInputDto input)
        {
            throw new NotImplementedException();
        }
    }
}
