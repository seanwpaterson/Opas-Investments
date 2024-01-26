using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.PortolioService.Models;

namespace Opas.Core.PortolioService.Configuration;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        _ = builder.ToTable("portfoliosPortfolios")
            .HasKey(x => x.Id);
    }
}
