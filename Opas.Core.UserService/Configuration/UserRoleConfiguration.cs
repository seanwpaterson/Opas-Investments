using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		_ = builder.ToTable("usersUserRoles")
			.HasKey(u => new { u.RoleId, u.UserId });
	}
}
