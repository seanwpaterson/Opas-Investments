﻿using Opas.Core.Data.Models;
using Opas.Core.UserService.Models.Users;

namespace Opas.Core.UserService.Repositories;

public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
{
	public UserTokenRepository(UserDbContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public override string GetName(UserToken entity)
	{
		return entity.Name;
	}
}
