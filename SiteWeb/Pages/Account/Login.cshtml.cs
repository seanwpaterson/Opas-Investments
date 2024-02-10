using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Models.Users;

namespace SiteWeb.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly IUserService _userService;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(
        IUserService userService,
        ILogger<LoginModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [BindProperty]
    public LoginFormModel Input { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(returnUrl))
        {
            ModelState.AddModelError(string.Empty, returnUrl);
        }

        returnUrl ??= Url.Content("~/");

        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var result = await _userService.PasswordSignInAsync(Input.Email,
                Input.Password, Input.RememberMe);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");

                if (returnUrl == null)
                {
                    return RedirectToPage("./Account");
                }

                if (Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return Redirect("/");
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out");

                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return Page();
            }
        }

        return Page();
    }
}
