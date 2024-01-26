using Opas.Core.Data.Models;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Repositories;

public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
	public UserRoleRepository(UserDbContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public override string GetName(UserRole entity)
	{
		return entity.RoleId;
	}
}
