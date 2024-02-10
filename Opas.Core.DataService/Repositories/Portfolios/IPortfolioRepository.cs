using Opas.Core.DataService.Models.Base;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Models.Users;

namespace Opas.Core.DataService.Repositories.Portfolios;

public interface IPortfolioRepository : IBaseIdentityRepository<Portfolio, User>
{
}
