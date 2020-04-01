using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared
{
    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder b);
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        Type DbContextType { get; }

        /// <summary>
        /// 获取 相应的实体类型
        /// </summary>
        Type EntityType { get; }
    }
}
