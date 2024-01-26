using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Configuration;
internal class UserRoleClaimConfiguration : IEntityTypeConfiguration<UserRoleClaim>
{
	public void Configure(EntityTypeBuilder<UserRoleClaim> builder)
	{
		_ = builder.ToTable("usersUserRoleClaims")
			.HasKey(u => u.Id);
	}
}
