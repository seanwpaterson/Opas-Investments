using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Models;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin;

public class IndexModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IAuthorizationAdminService _authorizationService;

    public List<MenuItem> MenuItems { get; set; }

    public IndexModel(IUserService userService, IAuthorizationAdminService authorizationService)
    {
        _userService = userService;
        _authorizationService = authorizationService;

        MenuItems = new List<MenuItem>();
    }

    public async Task<IActionResult> OnGet()
    {
        var isAuthorized = await _authorizationService.AuthorizeForAdminAsync(User);

        if (isAuthorized == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        var user = UserHelper.GetUserFromClaims(User, _userService);

        if (user == null)
        {
            return StatusCode(403);
        }

        MenuItems = await BuildMenuAsync(user);

        return Page();
    }

    private async Task<List<MenuItem>> BuildMenuAsync(User user)
    {
        var items = new List<MenuItem>();

        if (await _userService.IsInRoleAsync(user, UserHelper.PortfolioManagementRoleName))
        {
            items.Add(new MenuItem
            {
                Title = "Portfolios",
                Icon = "fa-file-invoice",
                Link = "/Account/Admin/Portfolios/Index"
            });
        }

        if (await _userService.IsInRoleAsync(user, UserHelper.UserManagementRoleName))
        {
            items.Add(new MenuItem
            {
                Title = "Users",
                Icon = "fa-solid fa-users",
                Link = "/Account/Admin/Users/Index"
            });
        }

        return items;
    }
}
