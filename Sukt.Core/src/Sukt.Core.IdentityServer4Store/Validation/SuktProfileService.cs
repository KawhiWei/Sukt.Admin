using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Sukt.Core.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sukt.Core.IdentityServer4Store.Validation
{
    public class SuktProfileService : IProfileService
    {
        private readonly UserManager<UserEntity> _userManager;
        public SuktProfileService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var usermodel = await _userManager.FindByIdAsync(context.Subject.Identity.GetSubjectId());
            var claims = new List<Claim>();
            claims.Add(new Claim("TenantId", usermodel?.TenantId.ToString()));
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            await Task.CompletedTask;
            context.IsActive = true;
        }

    }
}
