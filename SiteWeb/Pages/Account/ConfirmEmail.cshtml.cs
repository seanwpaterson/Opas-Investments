using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Opas.Core.DataService.Services.Users;
using System.Text;

namespace SiteWeb.Pages.Account;

public class ConfirmEmailModel : PageModel
{
    protected readonly IUserService _userService;

    public ConfirmEmailModel(IUserService userService)
    {
        _userService = userService;
    }

    [TempData]
    public string StatusMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _userService.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userService.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
        {
            StatusMessage = "<p>Thank you for confirming your email. We are so glad you have joined us.<br>Any queries should be directed to <a href=\"mailto::info@opasinvestments.com\">info@opasinvestments.com</a></p>";
        }
        else
        {
            StatusMessage = "<p>Error confirming your email.</p>";
        }

        return Page();
    }
}
