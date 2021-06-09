using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Modules;
using Sukt.TestBase;
using Xunit;

namespace Sukt.Core.Test
{
    public class StartupModulesTest : IntegratedTest<TestModules>
    {
        private TestModules test = null;

        public StartupModulesTest()
        {
            test = ServiceProvider.GetService<TestModules>();
        }

        [Fact]
        public void Test_TestModules()
        {
            Assert.True(test.ApplicationInitializationIsCalled);
            Assert.True(test.ConfigureServicesIsCalled);
        }
    }

    public class TestModules : SuktAppModule
    {
        public bool ConfigureServicesIsCalled { get; set; }
        public bool ApplicationInitializationIsCalled { get; set; }

        public override void ApplicationInitialization(ApplicationContext context)
        {
            ApplicationInitializationIsCalled = true;
            base.ApplicationInitialization(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            ConfigureServicesIsCalled = true;
            base.ConfigureServices(context);
        }
    }

    public class TestModules1 : SuktAppModule
    {
        public override void ApplicationInitialization(ApplicationContext context)
        {
            base.ApplicationInitialization(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            base.ConfigureServices(context);
        }
    }

    [SuktDependsOn(typeof(TestModules3))]
    public class TestModules2 : SuktAppModule
    {
        public override void ApplicationInitialization(ApplicationContext context)
        {
            base.ApplicationInitialization(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            base.ConfigureServices(context);
        }
    }

    [SuktDependsOn(typeof(TestModules4))]
    public class TestModules3 : SuktAppModule
    {
        public override void ApplicationInitialization(ApplicationContext context)
        {
            base.ApplicationInitialization(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            base.ConfigureServices(context);
        }
    }

    public class TestModules4 : SuktAppModule
    {
        public override void ApplicationInitialization(ApplicationContext context)
        {
            base.ApplicationInitialization(context);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            base.ConfigureServices(context);
        }
    }
}