using Sukt.Module.Core.Permission;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Permission
{
    public class AuthorityVerificationContract : IAuthorityVerification
    {
        public Task<bool> IsPermission(string url)
        {
            //throw new NotImplementedException();
            return Task.FromResult(true);
        }
    }
}