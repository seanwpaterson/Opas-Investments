using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Models.Users;

namespace SiteWeb.Pages.Account;

public class IndexModel : PageModel
{
    protected readonly IUserService _userService;

    public UserDetailsViewModel? UserDetails { get; set; }

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

        UserDetails = MapModel(user);

        if (UserDetails is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
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

        return RedirectToPage("/Account/ConfirmDelete");
    }

    private UserDetailsViewModel MapModel(User user)
    {
        return new UserDetailsViewModel
        {
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName ?? string.Empty,
            EmailAddress = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            AddressLine1 = user.AddressLine1 ?? string.Empty,
            AddressLine2 = user.AddressLine2 ?? string.Empty,
            City = user.City ?? string.Empty,
            County = user.County ?? string.Empty,
            Postcode = user.Postcode ?? string.Empty,
            CreatedDate = user.DateCreated,
            UpdatedDate = user.DateUpdated ?? null,
            IsAdmin = user.IsAdmin,
            Status = user.Status,
            MembershipStatus = user.MembershipStatus
        };
    }
}
