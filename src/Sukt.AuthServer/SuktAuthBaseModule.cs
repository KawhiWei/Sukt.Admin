using Microsoft.AspNetCore.Builder;
using Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Middleware;
using Sukt.AuthServer.Validation.ResourceOwnerPassword;
using Sukt.Module.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer
{
    public class SuktAuthBaseModule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddSuktAuthServer()
                .AddResourceOwnerValidator<DefaultResourceOwnerPasswordValidator>();
                //AddDefaultEndpoints()
                //            .AddValidationServices()
                //            .AddResponseGenerators()
                //            .AddDefaultSecretParsers()
                //            .AddClientStore<SuktApplicationStore>();
        }
        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseMiddleware<SuktAuthServerMiddleware>();
        }

    }
}
