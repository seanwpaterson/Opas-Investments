using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Pages.Account;

public class ConfirmDeleteModel : PageModel
{
    protected readonly IUserService _userService;

    public User? ApplicationUser { get; set; }

    [BindProperty]
    [Display(Name = "Tick this to confirm deletion")]
    public bool Confirm { get; set; }

    public ConfirmDeleteModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!_userService.IsSignedIn(HttpContext.User))
        {
            return RedirectToPage("/Account/Login");
        }

        ApplicationUser = await _userService.GetUserAsync(HttpContext.User);

        if (ApplicationUser is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!_userService.IsSignedIn(HttpContext.User))
        {
            return RedirectToPage("/Account/Login");
        }

        ApplicationUser = await _userService.GetUserAsync(HttpContext.User);

        if (ApplicationUser is null)
        {
            return NotFound();
        }

        if (Confirm != true)
        {
            ModelState.AddModelError(string.Empty, "Please confirm you want to delete your account.");
            return Page();
        }

        await _userService.DeleteAsync(ApplicationUser);

        return RedirectToPage("../");
    }
}
