using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SiteWeb.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly ILogger _logger;
		private readonly AuthMessageSenderOptions _options;

		public EmailSender(IOptions<AuthMessageSenderOptions> options,
			ILogger<EmailSender> logger)
		{
			_options = options.Value;
			_logger = logger;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			if (string.IsNullOrEmpty(_options.SenderEmail))
			{
				throw new Exception("Null SenderEmail");
			}

			if (string.IsNullOrEmpty(_options.SenderPassword))
			{
				throw new Exception("Null SenderPassword");
			}

			if (string.IsNullOrEmpty(_options.SmtpHost))
			{
				throw new Exception("Null SmtpHost");
			}

			if (string.IsNullOrEmpty(_options.Port.ToString()))
			{
				throw new Exception("Null Port");
			}

			if (string.IsNullOrEmpty(_options.SenderName))
			{
				throw new Exception("Null SenderName");
			}

			await Send(subject, htmlMessage, email);
		}

		public async Task Send(string subject, string message, string toEmail)
		{
			MimeMessage email = new MimeMessage();

			email.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));
			email.To.Add(new MailboxAddress("", toEmail));

			email.Subject = subject;
			email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = message
			};

			using SmtpClient smtp = new SmtpClient();

			smtp.CheckCertificateRevocation = false;

			await smtp.ConnectAsync(_options.SmtpHost, _options.Port, false);

			// Note: only needed if the SMTP server requires authentication
			await smtp.AuthenticateAsync(_options.SenderEmail, _options.SenderPassword);

			await smtp.SendAsync(email);
			await smtp.DisconnectAsync(true);
		}
	}
}
