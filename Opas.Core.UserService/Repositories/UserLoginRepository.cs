using Opas.Core.Data.Models;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Repositories;

public class UserLoginRepository : BaseRepository<UserLogin>, IUserLoginRepository
{
	public UserLoginRepository(UserDbContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public override string GetName(UserLogin entity)
	{
		return entity.ProviderDisplayName;
	}
}
