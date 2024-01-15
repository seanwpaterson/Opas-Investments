using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Models.Users;

namespace SiteWeb.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserDetailsViewModel? UserDetails { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                return RedirectToPage("/Account/Login");
            }

            ApplicationUser? user = _userManager.GetUserAsync(HttpContext.User).Result;

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
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                return RedirectToPage("/Account/Login");
            }

            ApplicationUser? user = _userManager.GetUserAsync(HttpContext.User).Result;

            if (user is null)
            {
                return NotFound();
            }

            return RedirectToPage("/Account/ConfirmDelete");
        }

        private UserDetailsViewModel MapModel(ApplicationUser user)
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
                Status = user.Status,
            };
        }
    }
}
