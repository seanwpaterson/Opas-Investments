using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.UserService.Models.Users;

public class UserRoleClaim
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string ClaimType { get; set; } = string.Empty;

	public string ClaimValue { get; set; } = string.Empty;

	[ForeignKey("Role")]
	[Required]
	public string RoleId { get; set; } = string.Empty;

	public virtual Role? Role { get; set; }
}