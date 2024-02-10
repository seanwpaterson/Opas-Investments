using Microsoft.AspNetCore.Identity;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Models.Users;
using System.Security.Claims;

namespace Opas.Core.DataService.Services.Users;
public interface IUserService
{
    // User Manager
    Task AddToRoleAsync(User user, string role);

    Task<IdentityResult> ConfirmEmailAsync(User user, string token);

    Task<IdentityResult> CreateAsync(User user, string password);

    Task DeleteAsync(User user);

    Task<User?> FindByEmailAsync(string email);

    Task<User?> FindByIdAsync(string userId);

    Task<string> GenerateEmailConfirmationTokenAsync(User user);

    Task<string> GeneratePasswordResetTokenAsync(User user);

    Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal);

    Task<string> GetUserIdAsync(User user);

    Task<IEnumerable<User>> GetUsersAsync();

    Task<bool> IsEmailConfirmedAsync(User user);

    Task<bool> IsInRoleAsync(User user, string roleId);

    Task RemoveFromRoleAsync(User user, string role);

    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

    Task<IdentityResult> UpdateAsync(User user);

    IQueryable<User> UsersQueryable();

    Task<IEnumerable<Portfolio>?> GetPortfoliosForUser(string userId);

    Task<bool> CanUserViewPortfolio(string userId, int portfolioId);

    // User Manager Options
    bool RequireConfirmedAccount();

    // Role Manager

    // Sign In Manager
    bool IsSignedIn(ClaimsPrincipal claimsPrincipal);

    Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent);

    Task SignInAsync(User user, bool isPersistent);

    Task SignOutAsync();
}
