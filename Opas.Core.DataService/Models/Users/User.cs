using Microsoft.AspNetCore.Identity;
using Opas.Core.DataService.Models.Portfolios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.DataService.Models.Users;

public class User : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [StringLength(450)]
    public Guid UniqueIdentifier { get; set; } = Guid.NewGuid();

    [StringLength(50, MinimumLength = 1)]
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 1)]
    [Required]
    public string LastName { get; set; } = string.Empty;

    [StringLength(255)]
    public string? AddressLine1 { get; set; }

    [StringLength(255)]
    public string? AddressLine2 { get; set; }

    [StringLength(35)]
    public string? City { get; set; }

    [StringLength(35)]
    public string? County { get; set; }

    [StringLength(8)]
    public string? Postcode { get; set; }

    public UserStatus Status { get; set; } = UserStatus.Unknown;

    public MembershipStatus MembershipStatus { get; set; } = MembershipStatus.None;

    public bool IsAdmin { get; set; } = false;

    public DateTime DateCreated { get; set; } = DateTime.Now;

    public DateTime? DateUpdated { get; set; } = DateTime.Now;

    public virtual List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}
