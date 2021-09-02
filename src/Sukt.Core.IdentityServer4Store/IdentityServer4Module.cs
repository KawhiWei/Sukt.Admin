using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.IdentityServer4Store.Store;
using Sukt.Core.IdentityServer4Store.Validation;
using Sukt.Module.Core.Modules;

namespace Sukt.Core.IdentityServerFourStore
{
    public class IdentityServer4Module : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            var build = service.AddIdentityServer(opt =>
            {
                opt.Events.RaiseErrorEvents = true;
                opt.Events.RaiseInformationEvents = true;
                opt.Events.RaiseFailureEvents = true;
                opt.Events.RaiseSuccessEvents = true;
            }).AddDeveloperSigningCredential().AddProfileService<SuktProfileService>();
            service.AddTransient<IClientStore, ClientStoreBase>();
            service.AddTransient<IResourceStore, ApiResourceStoreBase>();
            service.AddTransient<IPersistedGrantStore, PersistedGrantStoreBase>();
            service.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordBaseValidator>();
            //service.AddTransient<IAccountService, AccountService>();
            //service.AddTransient<IConsentService, ConsentService>();
        }
    }
}
