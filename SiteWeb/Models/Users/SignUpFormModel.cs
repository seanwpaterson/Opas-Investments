using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Models.Users;

public class SignUpFormModel
{
    [StringLength(256, MinimumLength = 5), EmailAddress, Required, Display(Name = "Email address")]
    public string Email { get; set; } = string.Empty;

    [StringLength(14, MinimumLength = 4), Phone, Required, Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
