using Sukt.Core.Dtos.LoginIdentity;
using Sukt.Core.Shared;
using Sukt.Core.Shared.OperationResult;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sukt.Core.Application.LoginIdentity
{
    public interface IIdentityContract : IScopedDependency
    {
        Task<(OperationResponse item, Claim[] cliams)> Login(LoginInputDto loginDto);
    }
}