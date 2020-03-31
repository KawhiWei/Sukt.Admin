using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.EntityFrameworkCore.MappingConfiguration
{
    public class EntityManager : IEntityManager
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
