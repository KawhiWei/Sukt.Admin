using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.MongoDB.Infrastructure
{
    public class MongoDbContextOptions : IMongoDbContextOptions
    {
        public string ConnectionString { get ; set ; }
    }
}
