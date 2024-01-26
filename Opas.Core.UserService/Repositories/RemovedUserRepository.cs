using Opas.Core.Data.Models;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Repositories;

public class RemovedUserRepository : BaseRepository<RemovedUser>, IRemovedUserRepository
{
	public RemovedUserRepository(UserDbContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public override string GetName(RemovedUser entity)
	{
		return entity.UserName;
	}
}
