using Opas.Core.Data.Models;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
	public RoleRepository(UserDbContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public override string GetName(Role entity)
	{
		return entity.Name;
	}
}
