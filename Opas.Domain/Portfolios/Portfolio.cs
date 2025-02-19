using Opas.Domain.Abstractions;
using Opas.Domain.Portfolios.Events;

namespace Opas.Domain.Portfolios;

public sealed class Portfolio : Entity
{
    private Portfolio(
        Guid id,
        Title title,
        Description description,
        DateTime datePublished,
        DateTime dateCreated,
        Guid fileId,
        List<Guid> userIds)
        : base(id)
    {
        Title = title;
        Description = description;
        DatePublished = datePublished;
        DateCreated = dateCreated;
        FileId = fileId;
        UserIds = userIds;
    }

    public Title Title { get; private set; }

    public Description Description { get; private set; }

    public DateTime DatePublished { get; private set; }

    public DateTime DateCreated { get; private set; }

    public DateTime? DateUpdated { get; private set; }

    public Guid FileId { get; private set; }

    public List<Guid> UserIds { get; private set; }

    public static Portfolio Create(
        Guid id,
        Title title,
        Description description,
        DateTime datePublished,
        DateTime dateCreated,
        Guid fileId,
        List<Guid> userIds)
    {
        var portfolio = new Portfolio(id, title, description, datePublished, dateCreated, fileId, userIds);

        portfolio.RaiseDomainEvent(new PortfolioCreatedDomainEvent(portfolio.Id));

        return portfolio;
    }
}