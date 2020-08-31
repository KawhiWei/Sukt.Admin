using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.MongoDB.Infrastructure
{
    public interface IMongoDbContextOptions
    {
        string ConnectionString { get; set; }
    }
}
