using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Opas.Core.DataService.Services.Users;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace SiteWeb.Pages.Account;

[AllowAnonymous]
public class ResendEmailConfirmationModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IEmailSender _emailSender;
    protected readonly ILogger<ResendEmailConfirmationModel> _logger;

    public ResendEmailConfirmationModel(IUserService userService,
        IEmailSender emailSender,
        ILogger<ResendEmailConfirmationModel> logger)
    {
        _userService = userService;
        _emailSender = emailSender;
        _logger = logger;

        Input = new InputModel();
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userService.FindByEmailAsync(Input.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return Page();
        }

        var userId = await _userService.GetUserIdAsync(user);
        var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { userId, code },
            protocol: Request.Scheme);

        if (callbackUrl == null)
        {
            var ex = new NullReferenceException(nameof(callbackUrl));
            _logger.LogError(ex, ex.Message);

            throw ex;
        }

        await _emailSender.SendEmailAsync(
            Input.Email,
            "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");

        return Page();
    }
}
