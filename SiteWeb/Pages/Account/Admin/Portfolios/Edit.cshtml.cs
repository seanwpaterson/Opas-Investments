using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Opas.Core.DataService.Infrastructure;
using Opas.Core.DataService.Models;
using Opas.Core.DataService.Services.Users;
using Opas.Core.PortolioService.Services.Portfolios;
using SiteWeb.Services;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Pages.Account.Admin.Portfolios;

public class EditModel : PageModel
{
    protected readonly IWebHostEnvironment _environment;
    protected readonly IPortfolioService _portfolioService;
    protected readonly IUserService _userService;
    protected readonly IAuthorizationAdminService _authorizationService;

    public int Id { get; set; }

    [BindProperty, Required, Display(Name = "Title")]
    public required string Title { get; set; }

    [BindProperty, Required, Display(Name = "Description")]
    public string? Description { get; set; }

    [BindProperty, Required, Display(Name = "Portfolio File")]
    public IFormFile? Portfolio { get; set; }

    [BindProperty, Required, Display(Name = "Date to publish")]
    public DateTime PublishDate { get; set; }

    public EditModel(IWebHostEnvironment environment,
        IPortfolioService portfolioService,
        IUserService userService,
        IAuthorizationAdminService authorizationService)
    {
        _environment = environment;
        _portfolioService = portfolioService;
        _userService = userService;
        _authorizationService = authorizationService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
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

        Id = id;

        var portfolio = await _portfolioService.GetPortfolioAsync(id);

        if (portfolio == null)
        {
            return StatusCode(404);
        }

        var file = Path.Combine(_environment.WebRootPath, "portfolios", portfolio.PortfolioFile.FileName);

        using var fileStream = System.IO.File.OpenRead(file);

        Title = portfolio.Title;
        Description = portfolio.Description;
        Portfolio = new FormFile(fileStream, 0, fileStream.Length, fileStream.Name, Path.GetFileName(fileStream.Name));
        PublishDate = portfolio.DatePublished;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
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

        Id = id;

        var file = Path.Combine(_environment.WebRootPath, "portfolios", Portfolio.FileName);

        using var fileStream = new FileStream(file, FileMode.Create);

        await Portfolio.CopyToAsync(fileStream);

        var portfolio = await _portfolioService.GetPortfolioAsync(id);

        if (portfolio == null)
        {
            return StatusCode(404);
        }

        portfolio.Title = Title;
        portfolio.Description = Description;
        portfolio.PortfolioFile = new UploadedFile
        {
            FileName = Portfolio.FileName,
            SizeInBytes = Portfolio.Length,
            Category = "Portfolio"
        };
        portfolio.DatePublished = PublishDate;
        portfolio.DateUpdated = DateTime.Now;

        await _portfolioService.UpdatedPortfolioAsync(portfolio);

        return RedirectToPage("/Account/Admin/Portfolios/Index");

    }
}
