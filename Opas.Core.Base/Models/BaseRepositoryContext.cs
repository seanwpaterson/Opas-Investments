using Microsoft.EntityFrameworkCore;

namespace Opas.Core.Data;

public class BaseRepositoryContext : DbContext
{
    public BaseRepositoryContext(DbContextOptions<BaseRepositoryContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected BaseRepositoryContext(DbContextOptions options)
    : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
