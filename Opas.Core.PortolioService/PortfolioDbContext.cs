using Microsoft.EntityFrameworkCore;
using Opas.Core.Data;
using Opas.Core.PortolioService.Configuration;

namespace Opas.Core.PortolioService;

public class PortfolioDbContext : BaseRepositoryContext
{
    public PortfolioDbContext(DbContextOptions<BaseRepositoryContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
    }
}
