using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Services.Users;
using Opas.Core.PortolioService.Services.Portfolios;

namespace SiteWeb.Pages.Account.Portfolios;

public class DetailsModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IPortfolioService _portfolioService;

    public Portfolio? Portfolio { get; set; }

    public DetailsModel(IUserService userService,
        IPortfolioService portfolioService)
    {
        _userService = userService;
        _portfolioService = portfolioService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (!_userService.IsSignedIn(HttpContext.User))
        {
            return RedirectToPage("/Account/Login");
        }

        var user = await _userService.GetUserAsync(HttpContext.User);

        if (user is null)
        {
            return StatusCode(404);
        }

        var canView = await _userService.CanUserViewPortfolio(user.Id, id);

        if (canView == false)
        {
            return StatusCode(403);
        }

        Portfolio = await _portfolioService.GetPortfolioAsync(id);

        if (Portfolio is null)
        {
            return StatusCode(404);
        }

        return Page();
    }
}
