using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService;

public class AdminUserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        _ = builder.ToTable("usersUserTokens").HasNoKey();
    }
}