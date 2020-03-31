using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.AutoMapper;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sukt.Core.Test
{
    public class SuktTestAutoMapper : IClassFixture<SuktWebApplicationFactory<SuktTestServerFixtureBase>>
    {
        private readonly SuktWebApplicationFactory<SuktTestServerFixtureBase> _factory = null;
        public SuktTestAutoMapper(SuktWebApplicationFactory<SuktTestServerFixtureBase> factory)
        {
            _factory = factory;
        }
        /// <summary>
        /// 测试Auto Mapper
        /// </summary>
        [Fact]
        public void Test_AutoMapper()
        {
            var provider = _factory.Server.Services; //这垃圾测试
            Test test = new Test();
            test.Id = 1;
            test.Name = "测试10201";
            var dto = test.MapTo<TestDto>();
            Assert.NotNull(dto);
            Assert.Equal("测试10201", dto.Name);
        }
    }
    public class TestAutoMapperModuleBase : SuktMapperModuleBase
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return base.ConfigureServices(services);
        }
    }
    public class Test
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    [AutoMap(typeof(Test))]
    public class TestDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
