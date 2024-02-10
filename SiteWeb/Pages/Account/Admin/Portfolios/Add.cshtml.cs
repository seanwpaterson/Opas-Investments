using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Models;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.DataService.Models.Users;
using Opas.Core.DataService.Services.Users;
using Opas.Core.PortolioService.Services.Portfolios;
using SiteWeb.Services;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Pages.Account.Admin.Portfolios;

public class AddModel : PageModel
{
    protected readonly IWebHostEnvironment _environment;
    protected readonly IPortfolioService _portfolioService;
    protected readonly IUserService _userService;
    protected readonly IAuthorizationAdminService _authorizationService;

    [BindProperty, Display(Name = "Title")]
    public required string Title { get; set; }

    [BindProperty, Display(Name = "Description")]
    public required string Description { get; set; }

    [BindProperty, Display(Name = "Portfolio File")]
    public IFormFile? Portfolio { get; set; }

    [BindProperty, Display(Name = "Date to publish")]
    public DateTime PublishDate { get; set; }

    public AddModel(IWebHostEnvironment environment,
        IPortfolioService portfolioService,
        IUserService userService,
        IAuthorizationAdminService authorizationService)
    {
        _environment = environment;
        _portfolioService = portfolioService;
        _userService = userService;
        _authorizationService = authorizationService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (await _authorizationService.AuthorizeForAdminAsync(User) == false)
        {
            return StatusCode(403);
        }

        var user = UserHelper.GetUserFromClaims(User, _userService);

        if (user == null)
        {
            return StatusCode(403);
        }

        var isAuthorised = await _userService.IsInRoleAsync(user, UserHelper.PortfolioManagementRoleName);

        if (isAuthorised == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _authorizationService.AuthorizeForAdminAsync(User) == false)
        {
            return StatusCode(403);
        }

        var user = UserHelper.GetUserFromClaims(User, _userService);

        if (user == null)
        {
            return StatusCode(403);
        }

        var isAuthorised = await _userService.IsInRoleAsync(user, UserHelper.PortfolioManagementRoleName);

        if (isAuthorised == false)
        {
            return StatusCode(403);
        }

        ViewData["Layout"] = "Admin";

        if (!ModelState.IsValid || Portfolio is null)
        {
            return Page();
        }

        var file = Path.Combine(_environment.WebRootPath, "portfolios", Portfolio.FileName);

        using var fileStream = new FileStream(file, FileMode.Create);

        await Portfolio.CopyToAsync(fileStream);

        var portfolio = new Portfolio
        {
            Title = Title,
            Description = Description,
            PortfolioFile = new UploadedFile
            {
                FileName = Portfolio.FileName,
                SizeInBytes = Portfolio.Length,
                Category = "Portfolio"
            },
            DatePublished = PublishDate.Date,
            DateCreated = DateTime.Now,
            Users = _userService.UsersQueryable()
                .Where(x => (x.MembershipStatus == MembershipStatus.Subscribed ||
                    x.MembershipStatus == MembershipStatus.Trial) &&
                    x.Status == UserStatus.Active)
                .ToList()
        };

        await _portfolioService.AddPortfolioAsync(portfolio);

        return RedirectToPage("/Account/Admin/Portfolios/Index");

    }
}
