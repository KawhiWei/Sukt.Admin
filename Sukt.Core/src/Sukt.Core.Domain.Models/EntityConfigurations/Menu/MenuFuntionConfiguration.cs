using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Shared;
using System;

namespace Sukt.Core.Domain.Models.Menu
{
    /// <summary>
    /// 菜单功能模块数据库配置文件
    /// </summary>
    public class MenuFuntionConfiguration : EntityMappingConfiguration<MenuFunctionEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<MenuFunctionEntity> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.MenuId);
            b.Property(o => o.FunctionId);
            b.ToTable("MenuFunction");
        }
    }
}