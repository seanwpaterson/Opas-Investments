using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Services;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Pages.Account.Admin.Users;

public class ConfirmDeleteModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IAuthorizationAdminService _authorizationService;

    public User? ApplicationUser { get; set; }

    [BindProperty]
    [Display(Name = "Tick this to confirm deletion")]
    public bool Confirm { get; set; }

    public ConfirmDeleteModel(IUserService userService,
        IAuthorizationAdminService authorizationService)
    {
        _userService = userService;
        _authorizationService = authorizationService;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (await _authorizationService.AuthorizeForAdminAsync(User) == false)
        {
            return StatusCode(403);
        }

        var user = UserHelper.GetUserFromClaims(User, _userService);

        if (user == null)
        {
            return StatusCode(403);
        }

        var isAuthorised = await _userService.IsInRoleAsync(user, UserHelper.UserManagementRoleName);

        if (isAuthorised == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        ApplicationUser = await _userService.FindByIdAsync(id);

        if (ApplicationUser is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (await _authorizationService.AuthorizeForAdminAsync(User) == false)
        {
            return StatusCode(403);
        }

        var user = UserHelper.GetUserFromClaims(User, _userService);

        if (user == null)
        {
            return StatusCode(403);
        }

        var isAuthorised = await _userService.IsInRoleAsync(user, UserHelper.UserManagementRoleName);

        if (isAuthorised == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        ApplicationUser = await _userService.FindByIdAsync(id);

        if (ApplicationUser == null)
        {
            return NotFound();
        }

        if (Confirm != true)
        {
            ModelState.AddModelError(string.Empty, "Please confirm you want to delete this user.");
            return Page();
        }

        await _userService.DeleteAsync(ApplicationUser);

        return RedirectToPage("/Account/Admin/Users/Index");
    }
}
