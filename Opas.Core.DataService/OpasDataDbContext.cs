using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Opas.Core.DataService.Configuration.Portfolios;
using Opas.Core.DataService.Models;
using Opas.Core.DataService.Models.Base;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Models.Users;

namespace Opas.Core.DataService;

public class OpasDataDbContext : BaseIdentityRepositoryContext<User>
{
    public OpasDataDbContext(DbContextOptions<OpasDataDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    public DbSet<Portfolio> Portfolios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customer Users
        _ = modelBuilder.Entity<User>()
                .Property(a => a.Status)
                .HasConversion<short>();

        modelBuilder.Entity<User>()
            .Property(u => u.UserId).Metadata
            .SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

        _ = modelBuilder.Entity<IdentityRole>(b => _ = b.ToTable("usersRoles"));

        _ = modelBuilder.Entity<User>(b => _ = b.ToTable("usersUsers"));

        _ = modelBuilder.Entity<IdentityUserClaim<string>>(b => _ = b.ToTable("usersUserClaims"));

        _ = modelBuilder.Entity<IdentityUserLogin<string>>(b => _ = b.ToTable("usersUserLogins"));

        _ = modelBuilder.Entity<IdentityUserToken<string>>(b => _ = b.ToTable("usersUserTokens"));

        _ = modelBuilder.Entity<IdentityRoleClaim<string>>(b => _ = b.ToTable("usersRoleClaims"));

        _ = modelBuilder.Entity<IdentityUserRole<string>>(b => _ = b.ToTable("usersUserRoles"));

        // Portfolios
        _ = modelBuilder.ApplyConfiguration(new PortfolioConfiguration());

        _ = modelBuilder.Entity<UploadedFile>(b => _ = b.ToTable("portfoliosUploadedFile"));

    }
}