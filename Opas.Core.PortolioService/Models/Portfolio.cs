using Opas.Core.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opas.Core.PortolioService.Models;

public class Portfolio
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	public required string Title { get; set; }

	public string? Description { get; set; }

	[Required]
	public required UploadedFile PortfolioFile { get; set; }

	public DateTime DatePublished { get; set; }

	public int PublishedYear => DatePublished.Year;

	public int PublishedMonth => DatePublished.Month;

	public int MyProperty { get; set; }

	public DateTime DateCreated { get; set; }

	public DateTime DateUpdated { get; set; }
}
