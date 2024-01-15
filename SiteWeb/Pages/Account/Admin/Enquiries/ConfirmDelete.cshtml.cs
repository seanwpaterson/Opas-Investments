using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Data;
using SiteWeb.Models.Enquiries;
using SiteWeb.Services;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Pages.Account.Admin.Enquiries
{
	public class ConfirmDeleteModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IAuthorizationAdminService _authorizationService;

		public Enquiry? Enquiry { get; set; }

		[BindProperty]
		[Display(Name = "Tick this to confirm deletion")]
		public bool Confirm { get; set; }

		public ConfirmDeleteModel(ApplicationDbContext dbContext, IAuthorizationAdminService authorizationService)
		{
			_context = dbContext;
			_authorizationService = authorizationService;
		}

		public async Task<IActionResult> OnGetAsync(int id)
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}
			ViewData["Layout"] = "Admin";

			Enquiry = _context.Enquiry.Where(e => e.Id == id).FirstOrDefault();

			if (Enquiry is null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int id)
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}
			ViewData["Layout"] = "Admin";

			Enquiry = _context.Enquiry.Where(e => e.Id == id).FirstOrDefault();

			if (Enquiry == null)
			{
				return NotFound();
			}

			if (Confirm != true)
			{
				ModelState.AddModelError(string.Empty, "Please confirm you want to delete this enquiry.");
				return Page();
			}

			_context.Enquiry.Remove(Enquiry);
			await _context.SaveChangesAsync();

			return RedirectToPage("/Account/Admin/Enquiries/Index");
		}
	}
}
