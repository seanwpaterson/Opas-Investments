using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteWeb.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(255)]
        public string? AddressLine1 { get; set; }

        [StringLength(255)]
        public string? AddressLine2 { get; set; }

        [StringLength(35)]
        public string? City { get; set; }

        [StringLength(35)]
        public string? County { get; set; }

        [StringLength(8)]
        public string? Postcode { get; set; }

        public Status Status { get; set; } = Status.Unknown;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; } = DateTime.Now;
    }
}

public enum Status
{
    Unknown = 0,
    Registered = 1,
    Verified = 2,
    Subscribed = 3,
    Unsubscribed = 4,
    Admin = 9
}