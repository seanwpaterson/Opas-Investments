using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Services.Users;

namespace SiteWeb.Pages;

public class RegisterModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IEmailSender _emailSender;
    protected readonly ILogger<RegisterModel> _logger;

    public string? ReturnUrl { get; set; }

    public RegisterModel(IUserService userService,
        IEmailSender emailSender,
        ILogger<RegisterModel> logger)
    {
        _userService = userService;
        _emailSender = emailSender;
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}
