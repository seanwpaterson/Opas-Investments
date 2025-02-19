namespace Opas.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<User>> GetUsersAsync();

    void Add(User user);

    void Update(User user);
}