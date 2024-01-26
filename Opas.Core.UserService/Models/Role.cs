using System.ComponentModel.DataAnnotations;

namespace Opas.Core.UserService.Models.Users;

public class Role
{
	[Key]
	[StringLength(450)]
	public string Id { get; set; } = string.Empty;

	public string ConcurrencyStamp { get; set; } = string.Empty;

	[StringLength(256)]
	public string Name { get; set; } = string.Empty;

	[StringLength(256)]
	public string NormalizedName { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;
}