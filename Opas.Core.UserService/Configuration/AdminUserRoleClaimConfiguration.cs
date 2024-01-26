using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService;

public class AdminUserRoleClaimConfiguration : IEntityTypeConfiguration<UserRoleClaim>
{
    public void Configure(EntityTypeBuilder<UserRoleClaim> builder)
    {
        _ = builder.ToTable("usersAdminUserRoleClaims")
            .HasKey(u => u.Id);
    }
}