using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.Models;
using Sukt.Core.EntityFrameworkCore.Repositories;
using Sukt.Core.Identity;
using Sukt.Core.IdentityServer4Store.Validation;
using Sukt.Module.Core.Modules;
using System;

namespace Sukt.Core.AuthenticationCenter.Startups
{
    public class IdentityModule : SuktAppModule /*IdentityModuleBase<UserStore, RoleStore, UserEntity, UserRoleEntity, RoleEntity, Guid, Guid>*/
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScoped<IUserStore<UserEntity>, UserStore>();

            context.Services.AddScoped<IRoleStore<RoleEntity>, RoleStore>();
            Action<IdentityOptions> identityOption = IdentityOption();
            var identityBuilder = context.Services.AddIdentity<UserEntity, RoleEntity>(identityOption).AddClaimsPrincipalFactory<SuktClaimsPrincipalFactory>();
            context.Services.AddSingleton<IdentityErrorDescriber>(new IdentityErrorDescriberZhHans());
            UseIdentityBuilder(identityBuilder);
            AddAuthentication(context.Services);
        }
        protected void AddAuthentication(IServiceCollection services) { }
        protected Action<IdentityOptions> IdentityOption()
        {
            return options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            };
        }

        protected IdentityBuilder UseIdentityBuilder(IdentityBuilder identityBuilder)
        {
            return identityBuilder;/*.AddEntityFrameworkStores<DefaultDbContext>().AddDefaultTokenProviders();*/
        }
    }
}
