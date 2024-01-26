using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.UserService.Models.Users;

public class UserToken
{
	[ForeignKey("User")]
	public int UserId { get; set; }

	[StringLength(450)]
	public string LoginProvider { get; set; } = string.Empty;

	[StringLength(450)]
	public string Name { get; set; } = string.Empty;

	public string Value { get; set; } = string.Empty;
}