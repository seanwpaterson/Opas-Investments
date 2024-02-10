using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Services;

namespace SiteWeb.Pages.Account.Admin.Users;

public class DetailsModel : PageModel
{
    protected readonly IUserService _userService;
    protected readonly IAuthorizationAdminService _authorizationService;

    public User? ApplicationUser { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool IsAdmin { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool IsUserRole { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool IsPortfolioRole { get; set; }

    public DetailsModel(IUserService userService,
        IAuthorizationAdminService authorizationService)
    {
        _userService = userService;
        _authorizationService = authorizationService;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (await _authorizationService.AuthorizeForAdminAsync(User) == false)
        {
            return StatusCode(403);
        }

        var user = UserHelper.GetUserFromClaims(User, _userService);

        if (user == null)
        {
            return StatusCode(403);
        }

        var isAuthorised = await _userService.IsInRoleAsync(user, UserHelper.UserManagementRoleName);

        if (isAuthorised == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        ApplicationUser = await _userService.FindByIdAsync(id);

        if (ApplicationUser == null)
        {
            return StatusCode(404);
        }

        await GetRolesAsync(ApplicationUser);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (await _authorizationService.AuthorizeForAdminAsync(User) == false)
        {
            return StatusCode(403);
        }

        var user = UserHelper.GetUserFromClaims(User, _userService);

        if (user == null)
        {
            return StatusCode(403);
        }

        var isAuthorised = await _userService.IsInRoleAsync(user, UserHelper.UserManagementRoleName);

        if (isAuthorised == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var editUser = await _userService.FindByIdAsync(id);

        if (editUser == null)
        {
            return StatusCode(404);
        }

        var userStatus = Request.Form["user-status"];
        var membershipStatus = Request.Form["membership-status"];

        if (Enum.TryParse(userStatus, out UserStatus uStatus))
        {
            editUser.Status = uStatus;
        }

        if (Enum.TryParse(membershipStatus, out MembershipStatus mStatus))
        {
            editUser.MembershipStatus = mStatus;
        }

        editUser.IsAdmin = IsAdmin;

        if (IsAdmin == true)
        {
            if (await _userService.IsInRoleAsync(editUser, UserHelper.AdministratorRoleName) == false)
            {
                await _userService.AddToRoleAsync(editUser, UserHelper.AdministratorRoleName);
            }
        }
        else
        {
            if (await _userService.IsInRoleAsync(editUser, UserHelper.AdministratorRoleName) == true)
            {
                await _userService.RemoveFromRoleAsync(editUser, UserHelper.AdministratorRoleName);
            }
        }

        if (IsUserRole == true)
        {
            if (await _userService.IsInRoleAsync(editUser, UserHelper.UserManagementRoleName) == false)
            {
                await _userService.AddToRoleAsync(editUser, UserHelper.UserManagementRoleName);
            }
        }
        else
        {
            if (await _userService.IsInRoleAsync(editUser, UserHelper.UserManagementRoleName) == true)
            {
                await _userService.RemoveFromRoleAsync(editUser, UserHelper.UserManagementRoleName);
            }
        }

        if (IsPortfolioRole == true)
        {
            if (await _userService.IsInRoleAsync(editUser, UserHelper.PortfolioManagementRoleName) == false)
            {
                await _userService.AddToRoleAsync(editUser, UserHelper.PortfolioManagementRoleName);
            }
        }
        else
        {
            if (await _userService.IsInRoleAsync(editUser, UserHelper.PortfolioManagementRoleName) == true)
            {
                await _userService.RemoveFromRoleAsync(editUser, UserHelper.PortfolioManagementRoleName);
            }
        }

        editUser.DateUpdated = DateTime.Now;

        _ = await _userService.UpdateAsync(editUser);

        return RedirectToPage("/Account/Admin/Users/Details", id);
    }

    private async Task GetRolesAsync(User user)
    {
        IsAdmin = await _userService.IsInRoleAsync(user, UserHelper.AdministratorRoleName);
        IsUserRole = await _userService.IsInRoleAsync(user, UserHelper.UserManagementRoleName);
        IsPortfolioRole = await _userService.IsInRoleAsync(user, UserHelper.PortfolioManagementRoleName);
    }
}
