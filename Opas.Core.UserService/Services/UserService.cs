using Microsoft.EntityFrameworkCore;
using Opas.Core.UserService.Models.ViewModels;
using Opas.Core.UserService.Repositories;

namespace Opas.Core.UserService.Services;

public class UserService : IUserService
{
    protected readonly IUserRepository _userRepository;
    protected readonly IRoleRepository _roleRepository;
    protected readonly IUserRoleRepository _userRoleRepository;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public IQueryable<UserViewModel> GetUsers()
    {
        return _userRepository.Query()
            .Include(x => x.Roles)
            .Select(user => new UserViewModel
            {
                Id = user.Id,
                UniqueIdentifier = user.UniqueIdentifier,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Roles == null
                    ? new List<UserRoleViewModel>()
                    : user.Roles
                        .Select(role => new UserRoleViewModel
                        {
                            RoleId = role.RoleId,
                            Name = role.Role == null ? "No Role" : role.Role.Name,
                        }).ToList()
            });
    }

    public async Task<UserViewModel> GetUserAsync(int id)
    {
        var result = await GetUsers()
            .Where(x => x.Id == id)
            .Select(user => new UserViewModel
            {
                Id = user.Id,
                UniqueIdentifier = user.UniqueIdentifier,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Roles
            }).FirstOrDefaultAsync();

        if (result == null)
        {
            var message = string.Format("No User found for Id: {0}", id);
            throw new KeyNotFoundException(message);
        }

        return result;
    }

    public UserViewModel GetUser(int id)
    {
        var result = GetUsers()
            .Where(x => x.Id == id)
            .Select(user => new UserViewModel
            {
                Id = user.Id,
                UniqueIdentifier = user.UniqueIdentifier,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Roles
            }).FirstOrDefault();

        if (result == null)
        {
            var message = string.Format("No User found for Id: {0}", id);
            throw new KeyNotFoundException(message);
        }

        return result;
    }
}
