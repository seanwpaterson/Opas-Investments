using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteWeb.Data;
using SiteWeb.Models.Enquiries;
using SiteWeb.Models.Users;

namespace SiteWeb.Pages
{
	public class RegisterModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly ApplicationDbContext _context;

		[BindProperty]
		public EnquiryFormModel? Input { get; set; }

		public string? ReturnUrl { get; set; }

		public RegisterModel(UserManager<ApplicationUser> userManager,
			ILogger<RegisterModel> logger,
			IEmailSender emailSender,
			ApplicationDbContext dbContext)
		{
			_userManager = userManager;
			_logger = logger;
			_emailSender = emailSender;
			_context = dbContext;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPost(string? returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");

			Input ??= new EnquiryFormModel();

			DateTime submissionDateTime = DateTime.Now;

			if (ModelState.IsValid)
			{
				Enquiry enquiry = new Enquiry()
				{
					FirstName = Input.FirstName,
					LastName = Input.LastName,
					Email = Input.Email,
					PhoneNumber = Input.PhoneNumber,
					InvestmentPlatform = Input.InvestmentPlatform,
					Notes = Input.Notes,
					BestAvailability = Input.BestAvailability,
					EnquiryStatus = EnquiryStatus.New,
					CreatedDate = submissionDateTime,
				};

				await _context.Enquiry.AddAsync(enquiry);
				int result = await _context.SaveChangesAsync();

				if (result > 0)
				{
					_logger.LogInformation("User enquiry made.");

					return RedirectToPage("/EnquireSuccess");
				}
			}

			return Page();
		}
	}
}
