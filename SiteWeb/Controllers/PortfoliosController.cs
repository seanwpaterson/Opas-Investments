using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opas.Core.DataService.Models.Portfolios;
using Opas.Core.PortolioService.Services.Portfolios;
using SiteWeb.Models.Portfolios;

namespace SiteWeb.Controllers;

[Route("api/portfolios")]
[Produces("application/json")]
public class PortfoliosController : Controller
{
    protected readonly IPortfolioService _portfolioService;

    public PortfoliosController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpGet("all")]
    [ResponseCache(Duration = 60 * 10, Location = ResponseCacheLocation.Client, NoStore = false)]
    public async Task<IEnumerable<PortfolioTableViewModel>> GetAllInquiriesListing()
    {
        var portfolios = _portfolioService.PortfoliosQueryable();

        return await GetListingArrayAsync(portfolios);
    }

    protected async Task<PortfolioTableViewModel[]> GetListingArrayAsync(IQueryable<Portfolio> portfolios)
    {
        return await portfolios
            .Where(x => x != null)
            .Select(x => new PortfolioTableViewModel
            {
                Id = x.Id,
                Title = x.Title,
                DatePublished = x.DatePublished,
                DatePublishedHtml = x.DatePublished.ToShortDateString()
            })
            .ToArrayAsync();
    }
}
