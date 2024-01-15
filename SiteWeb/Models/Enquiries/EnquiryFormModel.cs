using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Models.Enquiries
{
    public class EnquiryFormModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Investment Platform")]
        [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string? InvestmentPlatform { get; set; }

        public string? Notes { get; set; }

        [Display(Name = "Best Availability")]
        public DateTime BestAvailability { get; set; }
    }
}
