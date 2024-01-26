using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opas.Core.EnquiryService.Models;

namespace Opas.Core.EnquiryService.Configuration;

public class EnquiryConfiguration : IEntityTypeConfiguration<Enquiry>
{
	public void Configure(EntityTypeBuilder<Enquiry> builder)
	{
		_ = builder.ToTable("enquiriesEnquiries")
			.HasKey(x => x.Id);
	}
}
