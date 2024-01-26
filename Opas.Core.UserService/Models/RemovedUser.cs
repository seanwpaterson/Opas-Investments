using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.UserService.Models.Users;

public class RemovedUser
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[StringLength(256, MinimumLength = 5)]
	public string Email { get; set; } = string.Empty;

	[StringLength(256, MinimumLength = 5)]
	public string NormalizedEmail { get; set; } = string.Empty;

	[StringLength(256)]
	public string NormalizedUserName { get; set; } = string.Empty;

	public string PhoneNumber { get; set; } = string.Empty;

	[StringLength(256)]
	public string UserName { get; set; } = string.Empty;

	public DateTime RegisteredOn { get; set; }

	public DateTime? LastLogin { get; set; }

	public int Status { get; set; }

	public bool IsSystemAdmin { get; set; }

	public int Flags { get; set; }
}