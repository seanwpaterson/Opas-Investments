using Opas.Domain.Abstractions;
using Opas.Domain.Users.Events;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Opas.Domain.Users;

public sealed class User : Entity
{
    private User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Address address,
        MembershipStatus membershipStatus,
        DateTime dateCreated,
        List<Guid> portfolioIds)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        MembershipStatus = membershipStatus;
        DateCreated = dateCreated;
        PortfolioIds = portfolioIds;
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Address Address { get; private set; }

    public MembershipStatus MembershipStatus { get; internal set; }

    public DateTime DateCreated { get; private set; }

    public DateTime? DateUpdated { get; internal set; }

    public List<Guid> PortfolioIds { get; private set; } = [];

    public static User Create(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Address address,
        MembershipStatus membershipStatus,
        DateTime dateCreated,
        List<Guid> portfolioIds)
    {
        var user = new User(id, firstName, lastName, address, membershipStatus, dateCreated, portfolioIds);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}