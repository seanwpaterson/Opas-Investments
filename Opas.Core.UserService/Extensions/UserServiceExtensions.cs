using Microsoft.Extensions.DependencyInjection;
using Opas.Core.UserService.Repositories;
using Opas.Core.UserService.Services;

namespace Opas.Core.UserService.Extensions;

public static class UserServiceExtensions
{
	public static IServiceCollection AddUserServices(this IServiceCollection services)
	{
		// Repositories
		_ = services.AddTransient<IUserRepository, UserRepository>();
		_ = services.AddTransient<IRoleRepository, RoleRepository>();
		_ = services.AddTransient<IUserRoleRepository, UserRoleRepository>();

		// Services
		_ = services.AddTransient<IUserService, Services.UserService>();

		return services;
	}
}
