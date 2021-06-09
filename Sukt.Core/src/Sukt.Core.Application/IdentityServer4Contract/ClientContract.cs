using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Domain.Services.IdentityServer4Domain.ClientDomainServices;
using Sukt.Core.Dtos.IdentityServer4Dto;
using Sukt.Core.Dtos.IdentityServer4Dto.Enums;
using Sukt.Module.Core;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application.IdentityServer4Contract
{
    public class ClientContract : IClientContract
    {
        private readonly IClientDomainService _clientDomainService;

        public ClientContract(IClientDomainService clientDomainService)
        {
            _clientDomainService = clientDomainService;
        }
        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> CreateAsync(ClientInputDto input)
        {
            input.NotNull(nameof(input));
            PrepareClientTypeForNewClient(input);
            var cliententity = new Client(input.ClientId, input.ClientName, input.AllowAccessTokensViaBrowser, input.AllowOfflineAccess);
            //var cliententity = new Client();
            cliententity.AddGrantTypes(input.AllowedGrantTypes);
            return await _clientDomainService.CreateAsync(cliententity);
        }
        /// <summary>
        /// 返回一个客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetLoadAsync(Guid id)
        {
            var entity = await _clientDomainService.GetLoadByIdAsync(id);
            return new OperationResponse("", entity, OperationEnumType.Success);
        }
        /// <summary>
        /// 客户端密钥
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> CreateSecretAsync(SecretInputDto input)
        {
            input.NotNull(nameof(input));
            var cliententity = await _clientDomainService.GetLoadByIdAsync(input.Id);
            //cliententity.AddClientSecrets(new ClientSecret("", input.Value.Sha256(), input.Type, null));
            return await _clientDomainService.UpdateAsync(cliententity);
        }
        /// <summary>
        /// 添加客户端允许访问范围
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResponse> CreateClientScopeAsync(CommonInputDto input)
        {
            input.NotNull(nameof(input));
            var cliententity = await _clientDomainService.GetLoadByIdAsync(input.Id);
            //cliententity.AddClientScopes(input.Allowed);
            return await _clientDomainService.UpdateAsync(cliententity);
        }
        /// <summary>
        /// 添加客户端退出登录Uri
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> CreatePostLogoutRedirectUriAsync(CommonInputDto input)
        {
            input.NotNull(nameof(input));
            var cliententity = await _clientDomainService.GetLoadByIdAsync(input.Id);
            //cliententity.AddPostLogoutRedirectUris(input.Allowed);
            return await _clientDomainService.UpdateAsync(cliententity);
        }
        public async Task<OperationResponse> CreateRedirectUriAsync(CommonInputDto input)
        {
            input.NotNull(nameof(input));
            var cliententity = await _clientDomainService.GetLoadByIdAsync(input.Id);
            //cliententity.AddRedirectUris(input.Allowed);
            return await _clientDomainService.UpdateAsync(cliententity);
        }
        public async Task<OperationResponse> CreateCorsOriginAsync(CommonInputDto input)
        {
            input.NotNull(nameof(input));
            var cliententity = await _clientDomainService.GetLoadByIdAsync(input.Id);
            //cliententity.AddCorsOrigins(input.Allowed);
            return await _clientDomainService.UpdateAsync(cliententity);
        }
        /// <summary>
        /// 客户端设置授权类型
        /// </summary>
        /// <param name="input"></param>
        private void PrepareClientTypeForNewClient(ClientInputDto input)
        {
            switch (input.ClientType)
            {
                case ClientTypeEnum.Implicit:
                    input.AllowedGrantTypes.AddRange(GrantTypes.Implicit);
                    break;
                case ClientTypeEnum.ImplicitAndClientCredentials:
                    input.AllowedGrantTypes.AddRange(GrantTypes.ImplicitAndClientCredentials);
                    break;
                case ClientTypeEnum.Code:
                    input.AllowedGrantTypes.AddRange(GrantTypes.Code);
                    break;
                case ClientTypeEnum.Hybrid:
                    input.AllowedGrantTypes.AddRange(GrantTypes.Hybrid);
                    break;
                case ClientTypeEnum.HybridAndClientCredentials:
                    input.AllowedGrantTypes.AddRange(GrantTypes.HybridAndClientCredentials);
                    break;
                case ClientTypeEnum.ClientCredentials:
                    input.AllowedGrantTypes.AddRange(GrantTypes.ClientCredentials);
                    break;
                case ClientTypeEnum.ResourceOwnerPassword:
                    input.AllowedGrantTypes.AddRange(GrantTypes.ResourceOwnerPassword);
                    break;
                case ClientTypeEnum.ResourceOwnerPasswordAndClientCredentials:
                    input.AllowedGrantTypes.AddRange(GrantTypes.ResourceOwnerPasswordAndClientCredentials);
                    break;
                case ClientTypeEnum.DeviceFlow:
                    input.AllowedGrantTypes.AddRange(GrantTypes.DeviceFlow);
                    break;
            }
        }
    }
}
