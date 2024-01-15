using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Models.Users;

namespace SiteWeb.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public UserDetailsEditModel? Input { get; set; }

        public EditModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
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

            if (HttpContext.User is null)
            {
                return RedirectToPage("/Account/Login");
            }

            ApplicationUser? user = _userManager.GetUserAsync(HttpContext.User).Result;

            if (user is null)
            {
                return NotFound();
            }

            Input = MapModel(user);

            if (Input is null)
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

            Input ??= new UserDetailsEditModel();

            if (ModelState.IsValid)
            {
                ApplicationUser? user = _userManager.GetUserAsync(HttpContext.User).Result;

                if (user is null)
                {
                    return NotFound();
                }

                user = MapModel(user, Input);

                user.DateUpdated = DateTime.Now;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Redirect("/Account");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private ApplicationUser MapModel(ApplicationUser user, UserDetailsEditModel userDetails)
        {
            user.UserName = userDetails.EmailAddress ?? string.Empty;
            user.NormalizedUserName = userDetails.EmailAddress?.ToUpper() ?? string.Empty;
            user.FirstName = userDetails.FirstName ?? string.Empty;
            user.LastName = userDetails.LastName ?? string.Empty;
            user.Email = userDetails.EmailAddress ?? string.Empty;
            user.NormalizedEmail = userDetails.EmailAddress?.ToUpper() ?? string.Empty;
            user.PhoneNumber = userDetails.PhoneNumber ?? string.Empty;
            user.AddressLine1 = userDetails.AddressLine1 ?? string.Empty;
            user.AddressLine2 = userDetails.AddressLine2 ?? string.Empty;
            user.City = userDetails.City ?? string.Empty;
            user.County = userDetails.County ?? string.Empty;
            user.Postcode = userDetails.Postcode ?? string.Empty;
            user.DateUpdated = userDetails.UpdatedDate ?? null;

            return user;
        }

        private UserDetailsEditModel MapModel(ApplicationUser user)
        {
            return new UserDetailsEditModel
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
