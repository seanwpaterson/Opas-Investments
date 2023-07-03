using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Members.Any()) return;

            var members = new List<Member>
            {
                new Member
                {
                   FirstName = "Augusto",
                   LastName = "Janssens",
                   EmailAddress = "a.janssens@mail.com",
                   PhoneNumber = "07123456789",
                   AddressLine1 = string.Empty,
                   AddressLine2 = string.Empty,
                   City = string.Empty,
                   County = string.Empty,
                   Postcode = string.Empty,
                   InvestmentPlatform = "Trading212",
                   Notes = string.Empty,
                   BestAvailability = DateTime.Now.AddDays(6),
                   DateCreated = DateTime.Now.AddDays(-1)
                },
                new Member
                {
                   FirstName = "Aldith",
                   LastName = "Abbà",
                   EmailAddress = "aldith_abbà@mail.com",
                   PhoneNumber = "07987654321",
                   AddressLine1 = string.Empty,
                   AddressLine2 = string.Empty,
                   City = string.Empty,
                   County = string.Empty,
                   Postcode = string.Empty,
                   InvestmentPlatform = "E-Toro",
                   Notes = string.Empty,
                   BestAvailability = DateTime.Now.AddDays(12),
                   DateCreated = DateTime.Now.AddDays(-4)
                },
                new Member
                {
                   FirstName = "Fenne",
                   LastName = "Beránek",
                   EmailAddress = "Fenne.Beránek@mail.com",
                   PhoneNumber = "07432156789",
                   AddressLine1 = string.Empty,
                   AddressLine2 = string.Empty,
                   City = string.Empty,
                   County = string.Empty,
                   Postcode = string.Empty,
                   InvestmentPlatform = "AJ Bell",
                   Notes = string.Empty,
                   BestAvailability = DateTime.Now.AddDays(17),
                   DateCreated = DateTime.Now.AddDays(-13)
                },
                new Member
                {
                   FirstName = "Helena",
                   LastName = "Heiman",
                   EmailAddress = "hheiman42@mail.com",
                   PhoneNumber = "07123456789",
                   AddressLine1 = string.Empty,
                   AddressLine2 = string.Empty,
                   City = string.Empty,
                   County = string.Empty,
                   Postcode = string.Empty,
                   InvestmentPlatform = "Stock Broker",
                   Notes = string.Empty,
                   BestAvailability = DateTime.Now.AddDays(52),
                   DateCreated = DateTime.Now.AddDays(-41)
                },
                new Member
                {
                   FirstName = "Silvie",
                   LastName = "Bernardino",
                   EmailAddress = "SilvieBernardino@mail.com",
                   PhoneNumber = "07123456789",
                   AddressLine1 = string.Empty,
                   AddressLine2 = string.Empty,
                   City = string.Empty,
                   County = string.Empty,
                   Postcode = string.Empty,
                   InvestmentPlatform = "Trading212",
                   Notes = string.Empty,
                   BestAvailability = DateTime.Now.AddDays(76),
                   DateCreated = DateTime.Now.AddDays(-102)
                },
                new Member
                {
                   FirstName = "Launce",
                   LastName = "Gagliardi",
                   EmailAddress = "launcegagliardi10@mail.com",
                   PhoneNumber = "07123456789",
                   AddressLine1 = string.Empty,
                   AddressLine2 = string.Empty,
                   City = string.Empty,
                   County = string.Empty,
                   Postcode = string.Empty,
                   InvestmentPlatform = "None",
                   Notes = string.Empty,
                   BestAvailability = DateTime.Now.AddDays(41),
                   DateCreated = DateTime.Now.AddDays(-13)
                },
            };

            await context.Members.AddRangeAsync(members);
            await context.SaveChangesAsync();
        }
    }
}