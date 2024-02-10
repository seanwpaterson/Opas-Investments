using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin.Users;

public class IndexModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IAuthorizationAdminService _authorizationService;

    public IndexModel(IUserService userService,
        IAuthorizationAdminService authorizationService)
    {
        _userService = userService;
        _authorizationService = authorizationService;
    }

    public async Task<IActionResult> OnGet()
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

        return Page();
    }
}
