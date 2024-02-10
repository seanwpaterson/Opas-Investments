using Opas.Core.DataService.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Models.Users;

public class UserDetailsEditModel
{
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? EmailAddress { get; set; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    [StringLength(255, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? AddressLine1 { get; set; }

    [StringLength(255, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? AddressLine2 { get; set; }

    [StringLength(35, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? City { get; set; }

    [StringLength(35, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? County { get; set; }

    [StringLength(8, ErrorMessage = "The {0} must be at max {1} characters long.")]
    public string? Postcode { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public UserStatus Status { get; set; }

    public MembershipStatus MembershipStatus { get; set; }
}
