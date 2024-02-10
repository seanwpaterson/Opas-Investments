using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin.Portfolios;

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

    public async Task<IActionResult> OnGetAsync()
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

        var isAuthorised = await _userService.IsInRoleAsync(user, UserHelper.PortfolioManagementRoleName);

        if (isAuthorised == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        return Page();
    }
}
