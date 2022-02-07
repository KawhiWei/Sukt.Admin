using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.PermissionManagement.Domain.Aggregates;

namespace Sukt.Admin.EntityFrameworkCore
{
    public class SuktAdminContext : SuktDbContextBase
    {
        public SuktAdminContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {

        }
        public DbSet<IdentityUser> IdentityUsers => Set<IdentityUser>();
        public DbSet<IdentityUserClaim> IdentityUserClaims => Set<IdentityUserClaim>();
        public DbSet<IdentityUserRole> IdentityUserRoles => Set<IdentityUserRole>();
        public DbSet<IdentityUserToken> IdentityUserTokens => Set<IdentityUserToken>();
        public DbSet<IdentityUserLogin> IdentityUserLogins => Set<IdentityUserLogin>();
        public DbSet<IdentityRole> IdentityRoles => Set<IdentityRole>();
        public DbSet<IdentityRoleClaim> IdentityRoleClaims => Set<IdentityRoleClaim>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Menu> Menus => Set<Menu>();
    }
}
