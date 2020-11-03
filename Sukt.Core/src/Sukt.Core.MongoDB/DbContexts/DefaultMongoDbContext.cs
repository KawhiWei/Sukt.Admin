using Sukt.Core.MongoDB.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace Sukt.Core.MongoDB.DbContexts
{
    public class DefaultMongoDbContext : MongoDbContextBase
    {
        public DefaultMongoDbContext([NotNull] MongoDbContextOptions options) : base(options)
        {
        }
    }
}