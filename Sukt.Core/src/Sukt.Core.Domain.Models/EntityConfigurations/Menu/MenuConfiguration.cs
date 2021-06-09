using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models.Menu
{
    /// <summary>
    /// 菜单模块数据库配置
    /// </summary>
    public class MenuConfiguration : EntityMappingConfiguration<MenuEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<MenuEntity> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.Name).HasMaxLength(50).IsRequired();
            b.Property(o => o.Component).HasMaxLength(200).IsRequired();
            b.Property(o => o.Icon).HasMaxLength(200).IsRequired();
            b.Property(o => o.ComponentName).HasMaxLength(200).IsRequired();
            b.Property(o => o.IsShow).HasDefaultValue(true);
            b.Property(o => o.ButtonClick).HasMaxLength(200).IsRequired();
            b.Property(o => o.Path).HasMaxLength(200);
            b.Property(o => o.Sort).HasDefaultValue(0);
            b.ToTable("Menu");
        }
    }
}