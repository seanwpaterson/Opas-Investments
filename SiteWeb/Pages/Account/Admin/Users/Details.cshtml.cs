using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Models.Users;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin.Users
{
	public class DetailsModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAuthorizationAdminService _authorizationService;


		public ApplicationUser? ApplicationUser { get; set; }

		public DetailsModel(UserManager<ApplicationUser> userManager, IAuthorizationAdminService authorizationService)
		{
			_userManager = userManager;
			_authorizationService = authorizationService;
		}

		public async Task<IActionResult> OnGetAsync(string id)
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}

			ViewData["Layout"] = "Admin";

			ApplicationUser = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();

			if (ApplicationUser is null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string id)
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}

			return RedirectToPage("/Account/Admin/Users/ConfirmDelete", new { Id = id });
		}
	}
}
