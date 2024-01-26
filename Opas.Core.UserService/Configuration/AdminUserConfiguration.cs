using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService;

public class AdminUserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder.ToTable("usersAdminUsers")
                .HasKey(u => u.Id);

        _ = builder.Property(c => c.Status).HasConversion<short>().IsRequired();
    }
}