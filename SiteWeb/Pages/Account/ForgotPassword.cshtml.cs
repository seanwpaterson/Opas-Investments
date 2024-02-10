using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Opas.Core.DataService.Services.Users;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace SiteWeb.Pages.Account;

public class ForgotPasswordModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IEmailSender _emailSender;
    protected readonly ILogger<ForgotPasswordModel> _logger;

    public ForgotPasswordModel(IUserService userService,
        IEmailSender emailSender,
        ILogger<ForgotPasswordModel> logger)
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByEmailAsync(Input.Email);

            if (user == null || !await _userService.IsEmailConfirmedAsync(user))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userService.GeneratePasswordResetTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { code },
                protocol: Request.Scheme);

            if (callbackUrl == null)
            {
                var ex = new NullReferenceException(nameof(callbackUrl));
                _logger.LogError(ex, ex.Message);

                throw ex;
            }

            await _emailSender.SendEmailAsync(
                Input.Email,
                "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        return Page();
    }
}
