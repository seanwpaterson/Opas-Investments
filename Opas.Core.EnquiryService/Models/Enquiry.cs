using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.EnquiryService.Models;

public class Enquiry
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string? FirstName { get; set; }

	[Required]
	[StringLength(50)]
	public string? LastName { get; set; }

	[Required]
	[StringLength(256)]
	public string? Email { get; set; }

	[Required]
	public string? PhoneNumber { get; set; }

	[StringLength(256)]
	public string? InvestmentPlatform { get; set; }

	public string? Notes { get; set; }

	public EnquiryStatus EnquiryStatus { get; set; }

	public DateTime? BestAvailability { get; set; }

	public DateTime CreatedDate { get; set; }

	public DateTime? UpdatedDate { get; set; }
}
