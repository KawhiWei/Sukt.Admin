using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Data.EntityFramework.Uwl_DbContext
{
    /// <summary>
    /// 基础设施层，持久化，数据访问、领域接口业务实现
    /// </summary>
    public class UwlDbContext:DbContext
    {
        //public UwlDbContext()
        //{
        //}

        public UwlDbContext(DbContextOptions<UwlDbContext> options) : base(options)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbBase, Migration.Migrations.Configuration>());
        }
        public DbSet<SysButton> UwlButtons { get; set; }
        public DbSet<SysMenu> UwlMenus { get; set; }
        public DbSet<SysOrganize> UwlSysOrganizes { get; set; }
        public DbSet<SysMenuButton> UwlMenuButtons { get; set; }
        public DbSet<SysRole> UwlRoles { get; set; }
        public DbSet<SysRoleGroup> UwlRoleGroups { get; set; }
        public DbSet<SysRoleRight> UwlRoleRights { get; set; }
        public DbSet<SysUser> UwlUsers { get; set; }
        public DbSet<SysSchedule> UwlSchedule { get; set; }
        public DbSet<SysUsersOrganizeRelation> UwlUsersOrganizeRelation { get; set; }
        public DbSet<SysUserRole> UwlUserRoles { get; set; }
        public DbSet<Logs> Log { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity< SysUserRole >().Property(e=>e.CreatedName).HasColumnType()
            //创建带有主外键关系的用户角色表
            //modelBuilder.Entity<SysButton>().MapSingleType()
            //.HasKey(ur => new { ur.UserId, ur.RoleId });
            ////创建带有主外键关系的角色菜单表
            //modelBuilder.Entity<RoleMenu>()
            //.HasKey(rm => new { rm.RoleId, rm.MenuId });
            //modelBuilder.UsePropertyAccessMode(c=>c.)
            //过滤不需要映射的字段
            //modelBuilder.Entity<SysMenu>().Ignore(m => m.CreateAts);
            base.OnModelCreating(modelBuilder);
        }
    }
    //public class BloggingContext : DbContext
    //{
    //    //public UwlDbContext(DbContextOptions<UwlDbContext> options) : base(options)
    //    //{
    //    //}
    //    //public DbSet<Department> Departments { get; set; }
    //    //public DbSet<SysMenu> Menus { get; set; }
    //    //public DbSet<Role> Roles { get; set; }
    //    //public DbSet<SysUser> Users { get; set; }
    //    //public DbSet<UserRole> UserRoles { get; set; }
    //    //public DbSet<RoleMenu> RoleMenus { get; set; }
    //    //protected override void OnModelCreating(DbContextOptionsBuilder modelBuilder)
    //    //    => modelBuilder.UseLoggerFactory().usesql
    //    //{
    //    //    ////创建带有主外键关系的用户角色表
    //    //    //modelBuilder.Entity<UserRole>()
    //    //    //.HasKey(ur => new { ur.UserId, ur.RoleId });
    //    //    ////创建带有主外键关系的角色菜单表
    //    //    //modelBuilder.Entity<RoleMenu>()
    //    //    //.HasKey(rm => new { rm.RoleId, rm.MenuId });
    //    //    ////modelBuilder.HasPostgresExtension();
    //    //    //base.OnModelCreating(modelBuilder);
    //    //}
    //}
}
