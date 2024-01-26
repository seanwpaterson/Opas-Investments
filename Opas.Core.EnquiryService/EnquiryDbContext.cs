using Microsoft.EntityFrameworkCore;
using Opas.Core.Data;
using Opas.Core.EnquiryService.Configuration;

namespace Opas.Core.EnquiryService;

public class EnquiryDbContext : BaseRepositoryContext
{
	public EnquiryDbContext(DbContextOptions<BaseRepositoryContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		_ = modelBuilder.ApplyConfiguration(new EnquiryConfiguration());
	}
}