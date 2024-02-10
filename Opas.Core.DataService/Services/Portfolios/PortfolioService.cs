using Microsoft.EntityFrameworkCore;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Repositories.Portfolios;

namespace Opas.Core.PortolioService.Services.Portfolios;

public class PortfolioService : IPortfolioService
{
    protected readonly IPortfolioRepository _portfolioRepository;

    public PortfolioService(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<Portfolio> GetPortfolioAsync(int id)
    {
        var result = await _portfolioRepository.Query()
            .Include(x => x.PortfolioFile)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            var message = string.Format("No Portfolio found for Id: {0}", id);
            throw new KeyNotFoundException(message);
        }

        return result;
    }

    public Portfolio GetPortfolio(int id)
    {
        var result = _portfolioRepository.Query()
            .FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            var message = string.Format("No Portfolio found for Id: {0}", id);
            throw new KeyNotFoundException(message);
        }

        return result;
    }

    public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync()
    {
        return await _portfolioRepository.Query()
            .ToArrayAsync();
    }

    public IEnumerable<Portfolio> GetPortfolios()
    {
        return _portfolioRepository.Query()
            .ToArray();
    }

    public async Task AddPortfolioAsync(Portfolio portfolio)
    {
        _ = await _portfolioRepository.AddAsync(portfolio);

        return;
    }

    public void AddPortfolio(Portfolio portfolio)
    {
        _ = _portfolioRepository.Add(portfolio);

        return;
    }

    public async Task UpdatedPortfolioAsync(Portfolio portfolio)
    {
        await _portfolioRepository.UpdateAsync(portfolio);

        return;
    }

    public void UpdatedPortfolio(Portfolio portfolio)
    {
        _portfolioRepository.Update(portfolio);

        return;
    }

    public async Task DeletePortfolioAsync(Portfolio portfolio)
    {
        await _portfolioRepository.DeleteAsync(portfolio);

        return;
    }

    public void DeletePortfolio(Portfolio portfolio)
    {
        _portfolioRepository.Delete(portfolio);

        return;
    }

    public IQueryable<Portfolio> PortfoliosQueryable()
    {
        return _portfolioRepository.Query();
    }
}
