using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Sukt.Module.Core.Attributes;
using Sukt.Module.Core.Entity;
using Sukt.TestBase;
using Sukt.MongoDB.Repositorys;
using Sukt.Module.Core.Filter;
using Sukt.Module.Core.ExpressionUtil;
using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.MongoDB;
using Sukt.MongoDB.DbContexts;

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

        [Fact]
        public async Task GetPageAsync_Test()
        {
            FilterCondition condition = new FilterCondition();
            QueryFilter filter = new QueryFilter();
            //condition.Field = "Name";
            //condition.Value = "大黄瓜18CM";
            //filter.Conditions.Add(condition);
            var exp = FilterHelp.GetExpression<TestDB>(filter);
            OrderCondition[] orderConditions = new OrderCondition[] {
                new OrderCondition("Name",Sukt.Module.Core.Enums.SortDirectionEnum.Descending),
                new OrderCondition("CreatedTime")
               };
            PagedRequest pagedRequest = new PagedRequest();
            pagedRequest.OrderConditions = orderConditions;
            var page = await _mongoDBRepository.Collection.ToPageAsync(exp, pagedRequest);

            Assert.True(page.Data.Count == 10);
            var page1 = await _mongoDBRepository.Collection.ToPageAsync(exp, pagedRequest, o => new TestDto
            {
                Id = o.Id,
                Name = o.Name
            });
            Assert.True(page1.Data.Count == 10);
        }
    }

    public class PagedRequest : IPagedRequest
    {
        public PagedRequest()
        {
            PageIndex = 1;
            PageRow = 10;
            OrderConditions = new OrderCondition[] { };
        }

        public int PageIndex { get; set; }
        public int PageRow { get; set; }
        public OrderCondition[] OrderConditions { get; set; }
    }

    public class TestDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
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
            var connection = File.ReadAllText(dbcontext).Trim();

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
        public Guid CreatedId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? LastModifyId { get; set; }
        public DateTime LastModifedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}