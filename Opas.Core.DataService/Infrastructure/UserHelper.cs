using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using System.Security.Claims;

namespace Opas.Core.DataService.Infrastructure;

public static class UserHelper
{
    public static string AdministratorRoleName = "Administrator";

    public static string UserManagementRoleName = "User Management";

    public static string PortfolioManagementRoleName = "Portfolio Management";

    internal static readonly string _nameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

    internal static readonly string _idClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    internal static readonly string _localUserIdClaimType = "localUserId";

    internal static readonly string _localRolesClaimType = "localRoles";

    public static string GetUserNameFromClaims(ClaimsPrincipal principle)
    {
        return principle?.Claims?.FirstOrDefault((Claim c) => _idClaimType.Equals(c.Type))?.Value ?? string.Empty;
    }

    public static string GetNameFromClaims(ClaimsPrincipal principle)
    {
        return principle?.Claims?.FirstOrDefault((Claim c) => _nameClaimType.Equals(c.Type))?.Value ?? string.Empty;
    }

    public static string GetLocalRolesFromClaims(ClaimsPrincipal principle)
    {
        return principle?.Claims?.FirstOrDefault((Claim c) => _localRolesClaimType.Equals(c.Type))?.Value ?? string.Empty;
    }

    public static IEnumerable<string> GetLocalRolesListFromClaims(ClaimsPrincipal principle)
    {
        var text = principle.Claims?.FirstOrDefault((Claim c) => _localRolesClaimType.Equals(c.Type))?.Value;
        if (string.IsNullOrWhiteSpace(text))
        {
            return Array.Empty<string>();
        }

        return text.ToLower().Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static bool IsUserInRole(ClaimsPrincipal principle, string roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
        {
            return false;
        }

        var localRolesFromClaims = GetLocalRolesFromClaims(principle);
        if (string.IsNullOrWhiteSpace(localRolesFromClaims))
        {
            return false;
        }

        return localRolesFromClaims.ToLower().Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(roleId.ToLower());
    }

    public static int GetLocalUserIdFromClaims(ClaimsPrincipal principal, IUserService userService)
    {
        var name = principal?.Claims?.FirstOrDefault((Claim c) => _nameClaimType.Equals(c.Type))?.Value;
        if (name == null)
        {
            return -1;
        }

        if (int.TryParse(userService.UsersQueryable()
            .FirstOrDefault((User u) => name.Equals(u.UserName))?.Id.ToString(), out var result))
        {
            return result;
        }

        return -1;
    }

    public static User? GetUserFromClaims(ClaimsPrincipal principal, IUserService userService)
    {
        var name = principal?.Claims?.FirstOrDefault((Claim c) => _nameClaimType.Equals(c.Type))?.Value;
        if (name == null)
        {
            return null;
        }

        return userService.UsersQueryable().FirstOrDefault((User u) => name.Equals(u.UserName));
    }
}
