using Sukt.Core.Dtos.LoginIdentity;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sukt.Core.Application.LoginIdentity
{
    public interface IIdentityContract : IScopedDependency
    {
        Task<(OperationResponse item, Claim[] cliams)> Login(LoginInputDto loginDto);
    }
}