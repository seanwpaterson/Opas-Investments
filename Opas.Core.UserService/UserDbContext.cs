using Microsoft.EntityFrameworkCore;
using Opas.Core.Data;
using Opas.Core.UserService.Configuration;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService;

public class UserDbContext : BaseRepositoryContext
{
    public UserDbContext(DbContextOptions<BaseRepositoryContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RemovedUser> RemovedUsers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserRoleClaim> UserRoleClaims { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }

    public DbSet<User> AdminUsers { get; set; }
    public DbSet<RemovedUser> AdminRemovedUsers { get; set; }
    public DbSet<Role> AdminRoles { get; set; }
    public DbSet<UserLogin> AdminUserLogins { get; set; }
    public DbSet<UserRoleClaim> AdminUserRoleClaims { get; set; }
    public DbSet<UserRole> AdminUserRoles { get; set; }
    public DbSet<UserToken> AdminUserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfiguration(new RemovedUserConfiguration());
        _ = modelBuilder.ApplyConfiguration(new RoleConfiguration());
        _ = modelBuilder.ApplyConfiguration(new UserConfiguration());
        _ = modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        _ = modelBuilder.ApplyConfiguration(new UserRoleClaimConfiguration());
        _ = modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        _ = modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

        _ = modelBuilder.ApplyConfiguration(new AdminRemovedUserConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AdminRoleConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AdminUserConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AdminUserLoginConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AdminUserRoleClaimConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AdminUserRoleConfiguration());
        _ = modelBuilder.ApplyConfiguration(new AdminUserTokenConfiguration());
    }
}