using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService;

public class AdminRemovedUserConfiguration : IEntityTypeConfiguration<RemovedUser>
{
    public void Configure(EntityTypeBuilder<RemovedUser> builder)
    {
        _ = builder.ToTable("usersAdminRemovedUsers")
            .HasKey(u => u.Id);
    }
}