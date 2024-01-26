using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.UserService.Models.Users;

public class User
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[StringLength(450)]
	public string UniqueIdentifier { get; set; } = string.Empty;

	public int AccessFailedCount { get; set; }

	public string ConcurrencyStamp { get; set; } = string.Empty;

	[StringLength(256, MinimumLength = 5)]
	[Required]
	public string Email { get; set; } = string.Empty;

	public bool EmailConfirmed { get; set; }

	[StringLength(50, MinimumLength = 1)]
	[Required]
	public string FirstName { get; set; } = string.Empty;

	[StringLength(50, MinimumLength = 1)]
	[Required]
	public string LastName { get; set; } = string.Empty;

	public bool LockoutEnabled { get; set; }

	public DateTimeOffset? LockoutEnd { get; set; }

	[StringLength(256, MinimumLength = 5)]
	public string NormalizedEmail { get; set; } = string.Empty;

	[StringLength(256)]
	public string NormalizedUserName { get; set; } = string.Empty;

	[StringLength(14, MinimumLength = 4)]
	[Required]
	public string PhoneNumber { get; set; } = string.Empty;

	public bool PhoneNumberConfirmed { get; set; }

	public string SecurityStamp { get; set; } = string.Empty;

	public bool TwoFactorEnabled { get; set; }

	[StringLength(256)]
	public string UserName { get; set; } = string.Empty;

	public DateTimeOffset RegisteredOn { get; set; }

	public DateTimeOffset? LastLogin { get; set; }

	public UserStatus Status { get; set; }

	public bool IsSystemAdmin { get; set; }

	public int Flags { get; set; }

	public virtual List<UserRole>? Roles { get; set; }
}
