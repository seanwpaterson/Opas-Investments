using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Services.Users;

namespace SiteWeb.Pages.Account;

public class LogoutModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly ILogger<LogoutModel> _logger;

    public LogoutModel(IUserService userService,
        ILogger<LogoutModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public void OnGet()
    {
        if (_userService.GetUserAsync(HttpContext.User) == null)
        {
            _ = RedirectToPage("/Account/Login");
        }
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        await _userService.SignOutAsync();

        _logger.LogInformation("User logged out.");

        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return Redirect("/");
        }
    }
}
