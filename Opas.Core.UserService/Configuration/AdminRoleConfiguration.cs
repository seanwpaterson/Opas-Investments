using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService;

public class AdminRoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        _ = builder.ToTable("usersAdminRoles")
            .HasKey(u => u.Id);
    }
}