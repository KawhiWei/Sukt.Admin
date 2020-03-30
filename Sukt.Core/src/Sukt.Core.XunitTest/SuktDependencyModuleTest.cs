using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Attributes.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sukt.Core.XunitTest
{
    public class SuktDependencyModuleTest: IClassFixture<SuktWebApplicationFactory<SuktTestServerFixtureBase>>
    {
        private readonly SuktWebApplicationFactory<SuktTestServerFixtureBase> _factory = null;
        public SuktDependencyModuleTest(SuktWebApplicationFactory<SuktTestServerFixtureBase> factory)
        {
            _factory = factory;
        }
        [Fact]
        public void Test_BulkInjection()
        {
            var provider = _factory.Server.Services;
            var test = provider.GetService<ITestScopedService>();
            Assert.NotNull(test);
            var getTest = test.GetTest();
            Assert.Equal("Test", getTest);

            var testTransientService = provider.GetService<ITestTransientService>();
            Assert.NotNull(testTransientService);
            var transient = testTransientService.GetTransientService();
            Assert.NotNull(transient);

            var testSingleton = provider.GetService<TestSingleton>();
            Assert.NotNull(testSingleton);


            var testService = provider.GetService<ITestService<User>>();
            Assert.NotNull(testService);
        }
    }
    public interface ITestScopedService
    {
        string GetTest();
    }

    [Dependency(ServiceLifetime.Scoped)]
    public class TestScopedService : ITestScopedService
    {

        public string GetTest()
        {
            return "Test";
        }
    }

    public interface ITestTransientService
    {
        string GetTransientService();
    }


    [Dependency(ServiceLifetime.Transient)]
    public class TestTransientService : ITestTransientService
    {
        public string GetTransientService()
        {

            return "测试瞬时注入成功";
        }
    }

    [Dependency(ServiceLifetime.Singleton)]
    public class TestSingleton
    {


    }
    public interface ITestService<User>
    {
    }
    [Dependency(ServiceLifetime.Scoped)]
    public class TestService : ITestService<User>
    {

    }



    public class User
    {

    }
}
