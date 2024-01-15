namespace SiteWeb.Models.Enquiries
{
	public class EnquiryDetailsViewModel
	{
		public int Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? Email { get; set; }

		public string? PhoneNumber { get; set; }

		public string? InvestmentPlatform { get; set; }

		public DateTime? BestAvailability { get; set; }

		public string? Notes { get; set; }

		public DateTime CreatedDate { get; set; }

		public DateTime? UpdatedDate { get; set; }

		public EnquiryStatus Status { get; set; }
	}
}
