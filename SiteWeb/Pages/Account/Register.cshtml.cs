using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Models.Users;
using System.Text;
using System.Text.Encodings.Web;

namespace SiteWeb.Pages.Account;

public class RegisterModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IEmailSender _emailSender;
    protected readonly ILogger<RegisterModel> _logger;

    [BindProperty]
    public SignUpFormModel Input { get; set; }

    public string ReturnUrl { get; set; } = string.Empty;

    public RegisterModel(IUserService userService,
        IEmailSender emailSender,
        ILogger<RegisterModel> logger)
    {
        _userService = userService;
        _logger = logger;
        _emailSender = emailSender;

        Input = new SignUpFormModel();
    }

    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl ?? string.Empty;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        var submissionDateTime = DateTime.Now;

        if (ModelState.IsValid)
        {
            var user = new User()
            {
                Email = Input.Email,
                NormalizedEmail = Input.Email.ToUpper(),
                UserName = Input.Email,
                NormalizedUserName = Input.Email.ToUpper(),
                PhoneNumber = Input.PhoneNumber,
                DateCreated = submissionDateTime,
                DateUpdated = submissionDateTime,
                Status = UserStatus.Active,
                MembershipStatus = MembershipStatus.None
            };

            var result = await _userService.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var userId = await _userService.GetUserIdAsync(user);
                var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId, code, returnUrl },
                    protocol: Request.Scheme);

                if (callbackUrl == null)
                {
                    var ex = new NullReferenceException(nameof(callbackUrl));
                    _logger.LogError(ex, ex.Message);

                    throw ex;
                }

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userService.RequireConfirmedAccount())
                {
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                }
                else
                {
                    await _userService.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}
