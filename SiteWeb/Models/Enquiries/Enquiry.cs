// Ignore Spelling: Enquiry

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteWeb.Models.Enquiries
{
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

	public enum EnquiryStatus
	{
		Unknown = 0,
		New = 1,
		[Display(Name = "Replied to")]
		Replied = 2,
		[Display(Name = "Had meeting")]
		HadMeeting = 3,
		[Display(Name = "Became member")]
		BecameMember = 4,
		[Display(Name = "Not interested")]
		NotInterested = 5
	}
}
