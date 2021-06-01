using Sukt.Core.Dtos.LoginIdentity;
using SuktCore.Shared;
using SuktCore.Shared.OperationResult;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sukt.Core.Application.LoginIdentity
{
    public interface IIdentityContract : IScopedDependency
    {
        Task<(OperationResponse item, Claim[] cliams)> Login(LoginInputDto loginDto);
    }
}