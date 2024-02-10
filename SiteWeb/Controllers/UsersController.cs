using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using SiteWeb.Models.Users;
using System.Web;

namespace SiteWeb.Controllers;

[Route("api/users")]
[Produces("application/json")]
[ApiController]
public class UsersController : Controller
{
    protected readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("all")]
    [ResponseCache(Duration = 60 * 10, Location = ResponseCacheLocation.Client, NoStore = false)]
    public async Task<IEnumerable<UserTableViewModel>> GetAllUsersListing()
    {
        var users = _userService.UsersQueryable();

        return await GetListingArrayAsync(users);
    }

    protected async Task<UserTableViewModel[]> GetListingArrayAsync(IQueryable<User> users)
    {
        var usersArray = await users
            .Select(u => new UserTableViewModel
            {
                Id = u.Id,
                DisplayName = GetDisplayName(u),
                EmailAddress = string.IsNullOrWhiteSpace(u.Email) ? string.Empty : HttpUtility.HtmlEncode(u.Email),
                SubmittedDate = u.DateCreated,
                SubmittedDateHtml = GetSubmittedDateHtml(u),
                Status = u.Status,
                StatusHtml = GetUserStatusHtml(u),
                MembershipStatus = u.MembershipStatus,
                MembershipStatusHtml = GetMembershipStatusHtml(u)
            })
            .ToArrayAsync();

        return usersArray;
    }

    private static string GetDisplayName(User user)
    {
        if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
        {
            return $"User: {user.UserId}";
        }
        else
        {
            return $"{@user.FirstName} {@user.LastName}";
        }
    }

    protected static string GetUserStatusHtml(User user)
    {
        return user.Status switch
        {
            UserStatus.Unknown => "<span class=\"table-label label-color-blue\">\r\nUnknown\r\n</span>",
            UserStatus.Active => "<span class=\"table-label label-color-green\">\r\nActive\r\n</span>",
            UserStatus.InActive => "<span class=\"table-label label-color-yellow\">\r\nInactive\r\n</span>",
            UserStatus.Denied => "<span class=\"table-label label-color-black\">\r\nDenied\r\n</span>",
            UserStatus.Deleted => "<span class=\"table-label label-color-red\">\r\nDeleted\r\n</span>",
            _ => ""
        };
    }

    private static string GetMembershipStatusHtml(User user)
    {
        return user.MembershipStatus switch
        {
            MembershipStatus.None => "<span class=\"table-label label-color-yellow\">None</span>",
            MembershipStatus.Subscribed => "<span class=\"table-label label-color-green\">Subscribed</span>",
            MembershipStatus.Trial => "<span class=\"table-label label-color-blue\">Trial</span>",
            MembershipStatus.Canceled => "<span class=\"table-label label-color-red\">Canceled</span>",
            _ => ""
        };
    }

    protected static string GetSubmittedDateHtml(User user)
    {
        return user.DateCreated.ToShortDateString();
    }
}
