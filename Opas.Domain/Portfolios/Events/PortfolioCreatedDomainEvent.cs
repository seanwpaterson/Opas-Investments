using Opas.Domain.Abstractions;

namespace Opas.Domain.Portfolios.Events;

public sealed record PortfolioCreatedDomainEvent(Guid PortfolioId) : IDomainEvent;