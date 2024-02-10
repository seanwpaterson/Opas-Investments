using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Services.Users;
using Opas.Core.PortolioService.Services.Portfolios;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin.Portfolios;

public class DeleteModel : PageModel
{
    protected readonly IPortfolioService _portfolioService;
    protected readonly IUserService _userService;
    protected readonly IAuthorizationAdminService _authorizationService;

    public DeleteModel(IPortfolioService portfolioService,
    IUserService userService,
    IAuthorizationAdminService authorizationService)
    {
        _portfolioService = portfolioService;
        _userService = userService;
        _authorizationService = authorizationService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
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

        var portfolio = await _portfolioService.GetPortfolioAsync(id);

        if (portfolio == null)
        {
            return NotFound();
        }

        await _portfolioService.DeletePortfolioAsync(portfolio);

        return RedirectToPage("/Account/Admin/Portfolios/Index");
    }
}
