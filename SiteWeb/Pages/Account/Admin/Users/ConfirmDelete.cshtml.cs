using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Models.Users;
using SiteWeb.Services;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Pages.Account.Admin.Users
{
	public class ConfirmDeleteModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAuthorizationAdminService _authorizationService;

		public ApplicationUser? ApplicationUser { get; set; }

		[BindProperty]
		[Display(Name = "Tick this to confirm deletion")]
		public bool Confirm { get; set; }

		public ConfirmDeleteModel(UserManager<ApplicationUser> userManager, IAuthorizationAdminService authorizationService)
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
			ViewData["Layout"] = "Admin";

			ApplicationUser = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();

			if (ApplicationUser == null)
			{
				return NotFound();
			}

			if (Confirm != true)
			{
				ModelState.AddModelError(string.Empty, "Please confirm you want to delete this user.");
				return Page();
			}

			await _userManager.DeleteAsync(ApplicationUser);

			return RedirectToPage("/Account/Admin/Users/Index");
		}
	}
}
