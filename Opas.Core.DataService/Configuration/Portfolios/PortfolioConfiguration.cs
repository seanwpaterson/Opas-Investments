using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.DataService.Models.Portfolios;

namespace Opas.Core.DataService.Configuration.Portfolios;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        _ = builder.ToTable("portfoliosPortfolios")
            .HasKey(x => x.Id);
    }
}
