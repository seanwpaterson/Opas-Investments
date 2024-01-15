using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin.Enquiries
{
	public class IndexModel : PageModel
	{
		private readonly IAuthorizationAdminService _authorizationService;

		public IndexModel(IAuthorizationAdminService authorizationService)
		{
			_authorizationService = authorizationService;
		}

		public async Task<IActionResult> OnGet()
		{
			bool isAuthorized = await _authorizationService.AuthorizeForAdminAsync(this.User);

			if (isAuthorized == false)
			{
				return Forbid();
			}

			ViewData["Layout"] = "Admin";

			return Page();
		}
	}
}
