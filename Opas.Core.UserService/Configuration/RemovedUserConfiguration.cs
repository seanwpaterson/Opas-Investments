using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Configuration;

public class RemovedUserConfiguration : IEntityTypeConfiguration<RemovedUser>
{
	public void Configure(EntityTypeBuilder<RemovedUser> builder)
	{
		_ = builder.ToTable("usersRemovedUsers")
			.HasKey(u => u.Id);
	}
}
