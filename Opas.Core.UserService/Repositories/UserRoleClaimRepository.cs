using Opas.Core.Data.Models;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Repositories;

public class UserRoleClaimRepository : BaseRepository<UserRoleClaim>, IUserRoleClaimRepository
{
	public UserRoleClaimRepository(UserDbContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public override string GetName(UserRoleClaim entity)
	{
		return entity.RoleId;
	}
}
