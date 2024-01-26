namespace Opas.Core.UserService.Models.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }

    public string UniqueIdentifier { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public List<UserRoleViewModel>? Roles { get; set; }
}
