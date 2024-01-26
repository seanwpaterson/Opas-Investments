using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Configuration;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
	public void Configure(EntityTypeBuilder<UserLogin> builder)
	{
		_ = builder.ToTable("usersUserLogins")
			.HasKey(u => new { u.LoginProvider, u.ProviderKey });
	}
}
