using Opas.Core.Data.Models;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Repositories;
public class UserRepository : BaseRepository<User>, IUserRepository
{
	public UserRepository(UserDbContext context)
		: base(context)
	{
	}

	public override string GetName(User entity)
	{
		return entity.UserName;
	}
}
