using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Data;
using SiteWeb.Models.Enquiries;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin.Enquiries
{
	public class DetailsModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IAuthorizationAdminService _authorizationService;


		public EnquiryDetailsViewModel? Details { get; set; }

		public DetailsModel(ApplicationDbContext dbContext, IAuthorizationAdminService authorizationService)
		{
			_context = dbContext;
			_authorizationService = authorizationService;
		}
		public async Task<IActionResult> OnGet(int id)
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}

			ViewData["Layout"] = "Admin";

			Enquiry? enquiry = _context.Enquiry.Where(e => e.Id == id).FirstOrDefault();

			if (enquiry == null)
			{
				return NotFound();
			}

			Details = MapModel(enquiry);

			return Page();
		}

		public async Task<IActionResult> OnPost(int id)
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}

			Microsoft.Extensions.Primitives.StringValues newStatus = Request.Form["enquiry-status"];

			Enquiry? enquiry = _context.Enquiry.Where(e => e.Id == id).FirstOrDefault();

			if (enquiry is null)
			{
				return NotFound();
			}

			if (Enum.TryParse(newStatus, out EnquiryStatus status))
			{
				enquiry.EnquiryStatus = status;
				enquiry.UpdatedDate = DateTime.Now;
			}

			_context.Enquiry.Update(enquiry);
			await _context.SaveChangesAsync();

			return Redirect("/Account/Admin/Enquiries/Details/" + id);
		}

		private EnquiryDetailsViewModel MapModel(Enquiry enquiry)
		{
			return new EnquiryDetailsViewModel
			{
				Id = enquiry.Id,
				FirstName = enquiry.FirstName,
				LastName = enquiry.LastName,
				Email = enquiry.Email,
				PhoneNumber = enquiry.PhoneNumber,
				InvestmentPlatform = enquiry.InvestmentPlatform,
				Notes = enquiry.Notes,
				BestAvailability = enquiry.BestAvailability,
				CreatedDate = enquiry.CreatedDate,
				UpdatedDate = enquiry.UpdatedDate,
				Status = enquiry.EnquiryStatus
			};
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}

			return RedirectToPage("/Account/Admin/Enquiries/ConfirmDelete", new { Id = id });
		}
	}
}
