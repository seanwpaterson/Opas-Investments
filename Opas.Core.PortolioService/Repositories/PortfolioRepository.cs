using Opas.Core.Data.Models;
using Opas.Core.PortolioService.Models;

namespace Opas.Core.PortolioService.Repositories;

public class PortfolioRepository : BaseRepository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(PortfolioDbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public override string GetName(Portfolio entity)
    {
        return entity.Title;
    }
}
