using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Models.Users;
using System.Security.Claims;

namespace Opas.Core.DataService.Services.Users;

public class UserService : IUserService
{
    protected readonly UserManager<User> _userManager;
    protected readonly RoleManager<IdentityRole> _roleManager;
    protected readonly SignInManager<User> _signInManager;

    public UserService(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    // User Manager
    public async Task AddToRoleAsync(User user, string role)
    {
        _ = await _userManager.AddToRoleAsync(user, role);

        return;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
        return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task DeleteAsync(User user)
    {
        _ = await _userManager.DeleteAsync(user);

        return;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> FindByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal)
    {
        return await _userManager.GetUserAsync(claimsPrincipal);
    }

    public async Task<string> GetUserIdAsync(User user)
    {
        return await _userManager.GetUserIdAsync(user);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<bool> IsEmailConfirmedAsync(User user)
    {
        return await _userManager.IsEmailConfirmedAsync(user);
    }

    public async Task<bool> IsInRoleAsync(User user, string roleId)
    {
        return await _userManager.IsInRoleAsync(user, roleId);
    }

    public async Task RemoveFromRoleAsync(User user, string role)
    {
        _ = await _userManager.RemoveFromRoleAsync(user, role);

        return;
    }

    public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    public async Task<IdentityResult> UpdateAsync(User user)
    {
        return await _userManager.UpdateAsync(user);
    }

    public IQueryable<User> UsersQueryable()
    {
        return _userManager.Users;
    }

    public async Task<IEnumerable<Portfolio>?> GetPortfoliosForUser(string userId)
    {
        return await _userManager.Users
            .Include(u => u.Portfolios)
            .ThenInclude(p => p.PortfolioFile)
            .Where(u => u.Id == userId)
            .Select(u => u.Portfolios
                .Where(p => p.DatePublished.Date <= DateTime.Now.Date))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CanUserViewPortfolio(string userId, int portfolioId)
    {
        var user = await _userManager.Users
            .Include(u => u.Portfolios)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return false;
        }

        return user.Portfolios.Where(p => p.Id == portfolioId).Any();
    }

    // User Manager Options
    public bool RequireConfirmedAccount()
    {
        return _userManager.Options.SignIn.RequireConfirmedAccount;
    }

    // Role Manager

    // Sign In Manager
    public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
    {
        return _signInManager.IsSignedIn(claimsPrincipal);
    }

    public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent)
    {
        return await _signInManager.PasswordSignInAsync(email, password, isPersistent, true);
    }

    public async Task SignInAsync(User user, bool isPersistent)
    {
        await _signInManager.SignInAsync(user, isPersistent);

        return;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();

        return;
    }
}
