using Opas.Core.DataService.Models.Users;

namespace SiteWeb.Models.Users;

public class UserTableViewModel
{
    public string Id { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public DateTime SubmittedDate { get; set; }

    public string SubmittedDateHtml { get; set; } = string.Empty;

    public UserStatus Status { get; set; }

    public string StatusHtml { get; set; } = string.Empty;

    public MembershipStatus MembershipStatus { get; set; }

    public string MembershipStatusHtml { get; set; } = string.Empty;
}
