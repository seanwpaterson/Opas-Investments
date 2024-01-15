using Microsoft.AspNetCore.Identity;
using SiteWeb.Models.Users;
using System.Security.Claims;

namespace SiteWeb.Services;

public interface IAuthorizationAdminService
{
	Task<bool> AuthorizeForAdminAsync(ClaimsPrincipal principal);
}

public class AuthorizeAdminService : IAuthorizationAdminService
{
	protected const string AdministratorRoleId = "Administrator";
	protected const string UserManagementRoleId = "UMR";
	protected const string EnquiryManagementRoleId = "EMR";
	protected const string PortfolioManagementRoleId = "PMR";

	private readonly UserManager<ApplicationUser> _userManager;

	public AuthorizeAdminService(UserManager<ApplicationUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<bool> AuthorizeForAdminAsync(ClaimsPrincipal principal)
	{
		ApplicationUser? user = await _userManager.GetUserAsync(principal);

		if (user is null)
		{
			return false;
		}

		return await _userManager.IsInRoleAsync(user, AdministratorRoleId);
	}
}
