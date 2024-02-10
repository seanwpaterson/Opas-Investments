using System.ComponentModel.DataAnnotations;

namespace Opas.Core.DataService.Models.Users;

public enum UserStatus
{
    Unknown = 0,
    Active = 1,
    [Display(Name = "Inactive")]
    InActive = 2,
    Denied = 3,
    Deleted = 4
}
