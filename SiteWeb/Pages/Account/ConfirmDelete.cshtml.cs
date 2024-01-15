using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Pages.Account
{
	public class ConfirmDeleteModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public ApplicationUser? ApplicationUser { get; set; }

		[BindProperty]
		[Display(Name = "Tick this to confirm deletion")]
		public bool Confirm { get; set; }

		public ConfirmDeleteModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (!_signInManager.IsSignedIn(HttpContext.User))
			{
				return RedirectToPage("/Account/Login");
			}

			ApplicationUser = await _userManager.GetUserAsync(HttpContext.User);

			if (ApplicationUser is null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!_signInManager.IsSignedIn(HttpContext.User))
			{
				return RedirectToPage("/Account/Login");
			}

			ApplicationUser = await _userManager.GetUserAsync(HttpContext.User);

			if (ApplicationUser is null)
			{
				return NotFound();
			}

			if (Confirm != true)
			{
				ModelState.AddModelError(string.Empty, "Please confirm you want to delete your account.");
				return Page();
			}

			await _userManager.DeleteAsync(ApplicationUser);

			return RedirectToPage("../");
		}
	}
}
