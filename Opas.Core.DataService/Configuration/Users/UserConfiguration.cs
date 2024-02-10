using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.DataService.Models.Users;

namespace Opas.Core.DataService.Configuration.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder.ToTable("usersUsers")
                .HasKey(u => u.Id);

        _ = builder.Property(c => c.Status).HasConversion<short>().IsRequired();

        builder.Property(u => u.UserId).Metadata
                .SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
    }
}