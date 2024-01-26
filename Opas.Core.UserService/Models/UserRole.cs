using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.UserService.Models.Users;

public class UserRole
{
	[ForeignKey("User")]
	public int UserId { get; set; }

	[ForeignKey("Role")]
	[StringLength(450)]
	public string RoleId { get; set; } = string.Empty;

	public virtual User? User { get; set; }

	public virtual Role? Role { get; set; }
}
