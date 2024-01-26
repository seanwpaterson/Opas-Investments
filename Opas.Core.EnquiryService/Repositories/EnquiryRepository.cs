using Opas.Core.Data.Models;
using Opas.Core.EnquiryService.Models;

namespace Opas.Core.EnquiryService.Repositories;

public class EnquiryRepository : BaseRepository<Enquiry>, IEnquiryRepository
{
	public EnquiryRepository(EnquiryDbContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public override string GetName(Enquiry entity)
	{
		return string.Format("{0}-{1}", entity.Email, entity.Id);
	}
}
