using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteWeb.Data;
using SiteWeb.Models.Enquiries;
using System.Web;

namespace SiteWeb.Controllers
{
    [Route("api/enquiries")]
    [Produces("application/json")]
    [ApiController]
    public class EnquiriesController : Controller
    {
        private readonly DbSet<Enquiry> _enquiries;

        public EnquiriesController(ApplicationDbContext dbContext)
        {
            _enquiries = dbContext.Enquiry;
        }

        [HttpGet]
        public async Task<IEnumerable<EnquiryTableViewModel>> GetAllEnquiriesListing()
        {
            return await GetListingArrayAsync(_enquiries.AsQueryable());
        }

        protected async Task<EnquiryTableViewModel[]> GetListingArrayAsync(IQueryable<Enquiry> enquiries)
        {
            EnquiryTableViewModel[] enquiriesArray = await enquiries
                .Select(i => new EnquiryTableViewModel
                {
                    Id = i.Id,
                    FirstName = string.IsNullOrWhiteSpace(i.FirstName) ? string.Empty : HttpUtility.HtmlEncode(i.FirstName),
                    LastName = string.IsNullOrWhiteSpace(i.LastName) ? string.Empty : HttpUtility.HtmlEncode(i.LastName),
                    EmailAddress = string.IsNullOrWhiteSpace(i.Email) ? string.Empty : HttpUtility.HtmlEncode(i.Email),
                    PhoneNumber = string.IsNullOrWhiteSpace(i.PhoneNumber) ? string.Empty : HttpUtility.HtmlEncode(i.PhoneNumber),
                    SubmittedDate = i.CreatedDate,
                    SubmittedDateHtml = GetSubmittedDateHtml(i),
                    Status = i.EnquiryStatus,
                    StatusHtml = GetStatusHtml(i)
                })
                .ToArrayAsync();

            return enquiriesArray;
        }

        protected static string GetStatusHtml(Enquiry enquiry)
        {
            return enquiry.EnquiryStatus switch
            {
                EnquiryStatus.New => "<span class=\"table-label label-color-yellow\">\r\nNew\r\n</span>",
                EnquiryStatus.Replied => "<span class=\"table-label label-color-blue\">\r\nReplied To\r\n</span>",
                EnquiryStatus.HadMeeting => "<span class=\"table-label label-color-red\">\r\nHad Meeting\r\n</span>",
                EnquiryStatus.BecameMember => "<span class=\"table-label label-color-green\">\r\nBecame Member\r\n</span>",
                EnquiryStatus.NotInterested => "<span class=\"table-label label-color-black\">\r\nNot Interested\r\n</span>",
                _ => ""
            };
        }

        protected static string GetSubmittedDateHtml(Enquiry enquiry)
        {
            return enquiry.CreatedDate.ToShortDateString();
        }
    }
}
