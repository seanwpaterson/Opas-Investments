using Opas.Core.DataService.Models.Users;

namespace SiteWeb.Models.Users;

public class UserDetailsViewModel
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? PhoneNumber { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? County { get; set; }

    public string? Postcode { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsAdmin { get; set; } = false;

    public UserStatus Status { get; set; }

    public MembershipStatus MembershipStatus { get; set; }
}
