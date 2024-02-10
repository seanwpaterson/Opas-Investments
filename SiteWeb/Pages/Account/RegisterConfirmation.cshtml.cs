﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Opas.Core.DataService.Services.Users;
using System.Text;

namespace SiteWeb.Pages.Account;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IEmailSender _emailSender;

    public RegisterConfirmationModel(IUserService userService,
        IEmailSender emailSender)
    {
        _userService = userService;
        _emailSender = emailSender;
    }

    public string Email { get; set; } = string.Empty;

    public bool DisplayConfirmAccountLink { get; set; }

    public string? EmailConfirmationUrl { get; set; }

    public async Task<IActionResult> OnGetAsync(string email, string? returnUrl = null)
    {
        if (email == null)
        {
            return RedirectToPage("/Index");
        }

        returnUrl ??= Url.Content("~/");

        var user = await _userService.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound($"Unable to load user with email '{email}'.");
        }

        Email = email;

        if (DisplayConfirmAccountLink)
        {
            var userId = await _userService.GetUserIdAsync(user);
            var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId, code, returnUrl },
                protocol: Request.Scheme);
        }

        return Page();
    }
}
