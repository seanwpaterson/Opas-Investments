using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteWeb.Models.Enquiries;
using SiteWeb.Models.Users;

namespace SiteWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Enquiry> Enquiry { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder.Entity<ApplicationUser>()
                .Property(a => a.Status)
                .HasConversion<short>();

            builder.Entity<ApplicationUser>()
                .Property(u => u.UserId).Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);


            _ = builder.Entity<Enquiry>().Property(a => a.EnquiryStatus)
                .HasConversion<short>();

            _ = builder.Entity<Enquiry>()
                .ToTable("Enquiries")
                .HasKey(e => e.Id);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
