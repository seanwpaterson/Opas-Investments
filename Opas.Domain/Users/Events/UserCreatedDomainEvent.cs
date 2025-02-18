using Opas.Domain.Abstractions;

namespace Opas.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;