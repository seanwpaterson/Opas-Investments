using Opas.Core.EnquiryService.Repositories;

namespace Opas.Core.EnquiryService.Services;

public class EnquiryService : IEnquiryService
{
	public readonly IEnquiryRepository EnquiryRepository;

	public EnquiryService(IEnquiryRepository enquiryRepository)
	{
		EnquiryRepository = enquiryRepository;
	}
}
