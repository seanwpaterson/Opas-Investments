using Opas.Core.DataService.Models.Base;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Models.Users;

namespace Opas.Core.DataService.Repositories.Portfolios;

public class PortfolioRepository : BaseIdentityRepository<Portfolio, User>, IPortfolioRepository
{
    public PortfolioRepository(OpasDataDbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public override string GetName(Portfolio entity)
    {
        return entity.Title;
    }
}
