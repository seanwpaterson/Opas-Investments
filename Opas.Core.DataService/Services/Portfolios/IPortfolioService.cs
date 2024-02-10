using Opas.Core.DataService.Models.Portfolios;

namespace Opas.Core.PortolioService.Services.Portfolios;

public interface IPortfolioService
{
    Task<Portfolio> GetPortfolioAsync(int id);

    Portfolio GetPortfolio(int id);

    Task<IEnumerable<Portfolio>> GetPortfoliosAsync();

    IEnumerable<Portfolio> GetPortfolios();

    Task AddPortfolioAsync(Portfolio portfolio);

    void AddPortfolio(Portfolio portfolio);

    Task UpdatedPortfolioAsync(Portfolio portfolio);

    void UpdatedPortfolio(Portfolio portfolio);

    Task DeletePortfolioAsync(Portfolio portfolio);

    void DeletePortfolio(Portfolio portfolio);

    IQueryable<Portfolio> PortfoliosQueryable();
}
