using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sukt.Core.Domain.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sukt.Core.IdentityServer4Store.Validation
{
    public class SuktClaimsPrincipalFactory : IUserClaimsPrincipalFactory<UserEntity>
    {
        private readonly UserManager<UserEntity> _userManager;

        public SuktClaimsPrincipalFactory(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IOptions<IdentityOptions> optionsAccessor)
        {
            _userManager = userManager;
        }
        public async Task<ClaimsPrincipal> CreateAsync(UserEntity user)
        {
            var principal = await _userManager.FindByIdAsync(user.ToString());
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(new[] { new Claim("TenantId", user.TenantId.ToString()) });
            claimsIdentity.AddClaims(new[] { new Claim("UserType", user.UserType.ToString()) });
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return claimsPrincipal;
        }
    }
}
