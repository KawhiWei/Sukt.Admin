using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ClientDomainServices
{
    public interface IClientDomainService : IScopedDependency
    {
        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(Client client);
        /// <summary>
        /// 根据Id获取一个客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Client> GetLoadByIdAsync(Guid id);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(Client client);
    }
}
