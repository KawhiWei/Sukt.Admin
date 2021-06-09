using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sukt.Core.AuthenticationCenter.Startups;
using Sukt.Module.Core.Modules;

namespace Sukt.Core.AuthenticationCenter
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private string _uris = "https://suktcoreauth.destinycore.club";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<SuktAppWebModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.Use(async (ctx, next) =>
                {
                    ctx.SetIdentityServerOrigin(_uris);
                    ctx.SetIdentityServerBasePath(ctx.Request.PathBase.Value.TrimEnd('/'));
                    await next();
                });
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.InitializeApplication();
        }
    }
}
