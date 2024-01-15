namespace SiteWeb.Models.Users
{
	public class UserTableViewModel
	{
		public string Id { get; set; } = string.Empty;

		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public string EmailAddress { get; set; } = string.Empty;

		public DateTime SubmittedDate { get; set; }

		public string SubmittedDateHtml { get; set; } = string.Empty;

		public Status Status { get; set; }

		public string StatusHtml { get; set; } = string.Empty;
	}
}
