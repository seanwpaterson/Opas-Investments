namespace Opas.Domain.Portfolios;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Portfolio portfolio);

    void Update(Portfolio portfolio);
}