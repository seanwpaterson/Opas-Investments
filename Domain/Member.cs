using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Member
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string InvestmentPlatform { get; set; }

        public string Notes { get; set; }

        public DateTime BestAvailability { get; set; }

        public DateTime DateCreated { get; set; }
    }
}