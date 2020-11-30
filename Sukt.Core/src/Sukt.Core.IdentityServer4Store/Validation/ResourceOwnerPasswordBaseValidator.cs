using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Sukt.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.IdentityServer4Store.Validation
{
    public class ResourceOwnerPasswordBaseValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISystemClock _clock;
        private readonly SignInManager<UserEntity> _signInManager;

        public ResourceOwnerPasswordBaseValidator(UserManager<UserEntity> userManager, ISystemClock clock, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _clock = clock;
            _signInManager = signInManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);
            if (user == null)
                return;
            var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, true);
            if (!result.Succeeded)
                return;
            var resultcontext = new GrantValidationResult(
                    user.Id.ToString() ?? throw new ArgumentException("Subject ID not set", nameof(user.Id)),
                    OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime);
            context.Result = resultcontext;
        }
    }
}
