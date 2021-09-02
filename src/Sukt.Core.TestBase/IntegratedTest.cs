using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;
using System;

namespace Sukt.Core.TestBase
{
    public abstract class IntegratedTest<TStartupModule> : TestBaseWithServiceProvider where TStartupModule : ISuktAppModule
    {
        protected IModuleApplication Application { get; }

        protected IServiceProvider RootServiceProvider { get; }

        protected IServiceScope TestServiceScope { get; }

        protected override IServiceProvider ServiceProvider => Application.ServiceProvider;

        protected IntegratedTest()
        {
            var services = CreateServiceCollection();
            BeforeAddApplication(services);
            var application = services.AddApplication<TStartupModule>();
            Application = services.GetBuildService<IModuleApplication>();
            AfterAddApplication(services);
            RootServiceProvider = CreateServiceProvider(services);
            TestServiceScope = RootServiceProvider.CreateScope();
            ((StartupModuleRunner)Application).Initialize(TestServiceScope.ServiceProvider);
        }

        protected virtual IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection();
        }

        protected virtual void BeforeAddApplication(IServiceCollection services)
        {
        }

        protected virtual void AfterAddApplication(IServiceCollection services)
        {
        }

        protected virtual IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildServiceProviderFromFactory();
        }

        protected virtual IServiceProvider ConfigureProvider(Action<IServiceCollection> configure)
        {
            var services = new ServiceCollection();

            configure(services);

            return services.BuildServiceProviderFromFactory();
        }
    }
}