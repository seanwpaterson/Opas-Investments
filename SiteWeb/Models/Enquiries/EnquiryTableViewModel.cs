namespace SiteWeb.Models.Enquiries
{
	public class EnquiryTableViewModel
	{
		public int Id { get; set; }

		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public string EmailAddress { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public DateTime SubmittedDate { get; set; }

		public string SubmittedDateHtml { get; set; } = string.Empty;

		public EnquiryStatus Status { get; set; }

		public string StatusHtml { get; set; } = string.Empty;
	}
}
