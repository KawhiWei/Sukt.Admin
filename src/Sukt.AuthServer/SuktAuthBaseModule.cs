using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.PlatformAbstractions;
using Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Middleware;
using Sukt.AuthServer.Validation.ResourceOwnerPassword;
using Sukt.Module.Core.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer
{
    public class SuktAuthBaseModule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            //var rsaCert = new X509Certificate2("./keys/identityserver.test.rsa.p12", "changeit");
            //var rsaCert = new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("cert", "idsrv4.pfx"), "changeit"));
            context.Services.AddSuktAuthServer()
                .AddResourceOwnerValidator<DefaultResourceOwnerPasswordValidator>().
            AddDefaultEndpoints()
            .AddDeveloperSigningCredential()
                        .AddValidationServices()
                        .AddResponseGenerators()
                        .AddDefaultSecretParsers()
                        .AddClientStore<SuktApplicationStore>();
        }
        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseMiddleware<SuktAuthServerMiddleware>();
        }

    }
}
