namespace Opas.Domain.Users;

public record Address(
    string? AddressLine1,
    string? AddressLine2,
    string? City,
    string? County,
    string? Postcode);