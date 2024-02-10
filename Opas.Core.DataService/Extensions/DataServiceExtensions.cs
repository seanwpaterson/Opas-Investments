using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Repositories.Portfolios;
using Opas.Core.DataService.Services.Users;
using Opas.Core.PortolioService.Services.Portfolios;

namespace Opas.Core.DataService.Extensions;

public static class DataServiceExtensions
{
    public static IServiceCollection AddUserServices(this IServiceCollection services, string connectionString)
    {
        _ = services.AddDbContext<OpasDataDbContext>(options =>
            options.UseSqlServer(connectionString));

        _ = services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<OpasDataDbContext>()
            .AddDefaultTokenProviders();

        _ = services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedAccount = true;
        });

        _ = services.AddTransient<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection AddPortfolioServices(this IServiceCollection services)
    {
        _ = services.AddTransient<IPortfolioRepository, PortfolioRepository>();
        _ = services.AddTransient<IPortfolioService, PortfolioService>();

        return services;
    }
}
