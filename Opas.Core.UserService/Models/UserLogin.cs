using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.UserService.Models.Users;

public class UserLogin
{
	[Key]
	[Column(Order = 1)]
	[StringLength(450)]
	public string LoginProvider { get; set; } = string.Empty;

	[Key]
	[Column(Order = 2)]
	[StringLength(450)]
	public string ProviderKey { get; set; } = string.Empty;

	public string ProviderDisplayName { get; set; } = string.Empty;

	[ForeignKey("User")]
	public int UserId { get; set; }

	public virtual User? User { get; set; }
}