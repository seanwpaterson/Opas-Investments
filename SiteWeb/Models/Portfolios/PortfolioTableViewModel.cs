namespace SiteWeb.Models.Portfolios;

public class PortfolioTableViewModel
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public DateTime DatePublished { get; set; }

    public required string DatePublishedHtml { get; set; }
}
