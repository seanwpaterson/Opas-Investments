using Opas.Core.DataService.Services.Users;
using System.Security.Claims;

namespace SiteWeb.Services;

public interface IAuthorizationAdminService
{
    Task<bool> AuthorizeForAdminAsync(ClaimsPrincipal principal);
}

public class AuthorizeAdminService : IAuthorizationAdminService
{
    protected const string AdministratorRoleId = "Administrator";

    private readonly IUserService _userService;

    public AuthorizeAdminService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> AuthorizeForAdminAsync(ClaimsPrincipal principal)
    {
        var user = await _userService.GetUserAsync(principal);

        if (user is null)
        {
            return false;
        }

        return await _userService.IsInRoleAsync(user, AdministratorRoleId);
    }
}
