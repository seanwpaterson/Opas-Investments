using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Services.Users;

namespace SiteWeb.Pages.Account.Portfolios;

public class IndexModel : PageModel
{
    protected readonly IUserService _userService;

    public List<Portfolio> Portfolios { get; set; }

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!_userService.IsSignedIn(HttpContext.User))
        {
            return RedirectToPage("/Account/Login");
        }

        var user = await _userService.GetUserAsync(HttpContext.User);

        if (user is null)
        {
            return NotFound();
        }

        var portfolios = await _userService.GetPortfoliosForUser(user.Id);

        if (portfolios == null)
        {
            Portfolios = new List<Portfolio>();
        }
        else
        {
            Portfolios = portfolios.OrderByDescending(x => x.DatePublished).ToList();
        }

        return Page();
    }
}
