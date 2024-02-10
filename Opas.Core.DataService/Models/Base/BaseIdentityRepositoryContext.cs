using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Opas.Core.DataService.Models.Base;

public class BaseIdentityRepositoryContext<TUser> : IdentityDbContext<TUser> where TUser : IdentityUser
{
    public BaseIdentityRepositoryContext(DbContextOptions<BaseIdentityRepositoryContext<TUser>> options)
    : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected BaseIdentityRepositoryContext(DbContextOptions options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
