using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteWeb.Models.Users;
using System.Web;

namespace SiteWeb.Controllers
{
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<UserTableViewModel>> GetAllUsersListing()
        {
            return await GetListingArrayAsync(_userManager.Users);
        }

        protected async Task<UserTableViewModel[]> GetListingArrayAsync(IQueryable<ApplicationUser> users)
        {
            UserTableViewModel[] usersArray = await users
                .Select(u => new UserTableViewModel
                {
                    Id = u.Id,
                    FirstName = string.IsNullOrWhiteSpace(u.FirstName) ? string.Empty : HttpUtility.HtmlEncode(u.FirstName),
                    LastName = string.IsNullOrWhiteSpace(u.LastName) ? string.Empty : HttpUtility.HtmlEncode(u.LastName),
                    EmailAddress = string.IsNullOrWhiteSpace(u.Email) ? string.Empty : HttpUtility.HtmlEncode(u.Email),
                    SubmittedDate = u.DateCreated,
                    SubmittedDateHtml = GetSubmittedDateHtml(u),
                    Status = u.Status,
                    StatusHtml = GetStatusHtml(u)
                })
                .ToArrayAsync();

            return usersArray;
        }

        protected static string GetStatusHtml(ApplicationUser member)
        {
            return member.Status switch
            {
                Status.Registered => "<span class=\"table-label label-color-yellow\">\r\nRegistered\r\n</span>",
                Status.Verified => "<span class=\"table-label label-color-blue\">\r\nVerified\r\n</span>",
                Status.Unsubscribed => "<span class=\"table-label label-color-red\">\r\nUnsubscribed\r\n</span>",
                Status.Subscribed => "<span class=\"table-label label-color-green\">\r\nRegistered\r\n</span>",
                Status.Admin => "<span class=\"table-label label-color-black\">\r\nAdmin\r\n</span>",
                _ => ""
            };
        }

        protected static string GetSubmittedDateHtml(ApplicationUser user)
        {
            return user.DateCreated.ToShortDateString();
        }
    }
}
