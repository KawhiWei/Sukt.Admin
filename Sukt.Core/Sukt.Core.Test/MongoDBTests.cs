using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Linq;
using Sukt.Core.MongoDB;
using Sukt.Core.MongoDB.DbContexts;
using Sukt.Core.MongoDB.Repositorys;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Entity;
using Sukt.Core.TestBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sukt.Core.Test
{
    public class MongoDBTests : IntegratedTest<MongoDBModelule>
    {
        private readonly IMongoDBRepository<TestDB, Guid> _mongoDBRepository = null;
        public MongoDBTests()
        {
            _mongoDBRepository = ServiceProvider.GetService<IMongoDBRepository<TestDB, Guid>>();
        }
        [Fact]
        public async Task Insert_Test()
        {
            TestDB test = new TestDB();
            test.IsDeleted = false;
            test.CreatedAt = DateTime.Now;
            test.Name = "1as32d1as3d1as32d1";
            await _mongoDBRepository.InsertAsync(test);
            var entite = await _mongoDBRepository.Entities.Where(x => x.Id == test.Id).FirstOrDefaultAsync();
            Assert.True(entite.Name == "1as32d1as3d1as32d1");
        }

    }

    public class MongoDBModelule : MongoDBModuleBase
    {
        protected override void AddDbContext(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            var configuration = builder.AddJsonFile("appsettings.json").Build();
            var dbpath = configuration["SuktCore:DbContext:MongoDBConnectionString"];
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            var dbcontext = Path.Combine(basePath, dbpath);
            if (!File.Exists(dbcontext))
            {
                throw new Exception("未找到存放数据库链接的文件");
            }
            var connection =  File.ReadAllText(dbcontext).Trim();

            services.AddMongoDbContext<DefaultMongoDbContext>(options =>
            {
                options.ConnectionString = connection;
            });
        }
    }
    [MongoDBTable("TestDB")]//
    public class TestDB : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public TestDB()
        {
            Id = Guid.NewGuid();
        }
        public string Name { get; set; }
        public Guid? CreatedId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? LastModifyId { get; set; }
        public DateTime LastModifedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
