using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SRKFruitsWeb.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SRKFruitsWeb.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ContactModel> _logger;

        public ContactModel(IConfiguration configuration, ILogger<ContactModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [BindProperty]
        public ContactFormModel Form { get; set; } = new();

        // Drives the success toast / thank-you state on the page.
        public bool SubmissionSucceeded { get; set; }

        public void OnGet()
        {
            ApplySuccessFromTempData();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SubmissionSucceeded = false;
                return Page();
            }

            try
            {
                await SendEnquiryEmailAsync(Form);
                SubmissionSucceeded = true;

                // Prevents a form resubmission on refresh (Post/Redirect/Get pattern).
                TempData["ContactSuccess"] = true;
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                // Email is best-effort: we still acknowledge the enquiry to the visitor,
                // but log the failure so it can be followed up manually.
                _logger.LogError(ex, "Failed to send contact form email for {Email}", Form.Email);
                ModelState.AddModelError(string.Empty,
                    "We couldn't send your message automatically. Please call or WhatsApp us directly at +91 98400 73951.");
                SubmissionSucceeded = false;
                return Page();
            }
        }

        // Reads TempData set after a successful redirect, so a page refresh
        // doesn't re-trigger the toast or resubmit the form.
        public void ApplySuccessFromTempData()
        {
            if (TempData["ContactSuccess"] is bool success && success)
            {
                SubmissionSucceeded = true;
            }
        }

        private async Task SendEnquiryEmailAsync(ContactFormModel form)
        {
            var smtp = _configuration.GetSection("SmtpSettings");
            var host = smtp["Host"];
            var senderEmail = smtp["SenderEmail"];
            var senderPassword = smtp["SenderPassword"];
            var toEmail = smtp["ToEmail"];

            // If SMTP hasn't been configured yet, skip sending instead of throwing,
            // so the site still works out of the box during development.
            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(senderEmail)
                || senderEmail == "your-sending-address@gmail.com")
            {
                _logger.LogWarning("SMTP is not configured. Enquiry from {Email} was not emailed.", form.Email);
                return;
            }

            var port = smtp.GetValue<int>("Port", 587);
            var enableSsl = smtp.GetValue<bool>("EnableSsl", true);

            using var message = new MailMessage
            {
                From = new MailAddress(senderEmail, "SRK Fruits Website"),
                Subject = $"New Website Enquiry: {form.EnquiryType} — {form.FullName}",
                Body =
                    $"Name: {form.FullName}\n" +
                    $"Email: {form.Email}\n" +
                    $"Phone: {form.Phone}\n" +
                    $"Enquiry Type: {form.EnquiryType}\n\n" +
                    $"Message:\n{form.Message}",
                IsBodyHtml = false
            };
            message.To.Add(toEmail ?? senderEmail);
            message.ReplyToList.Add(new MailAddress(form.Email, form.FullName));

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = enableSsl
            };

            await client.SendMailAsync(message);
        }
    }
}
