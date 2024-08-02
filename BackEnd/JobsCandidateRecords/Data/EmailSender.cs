using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace JobsCandidateRecords.Data
{
    /// <summary>
    /// Implementation of <see cref="IEmailSender"/> for sending emails using SMTP.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="configuration">The configuration used to retrieve SMTP settings.</param>
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The body of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the SMTP port configuration is missing or invalid, or when the SMTP username is not configured.
        /// </exception>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Retrieve the SMTP port from configuration
            var portString = _configuration["Smtp:Port"];
            if (!int.TryParse(portString, out int port))
            {
                throw new InvalidOperationException("The SMTP port configuration is missing or invalid.");
            }

            // Create and configure the SmtpClient
            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                UseDefaultCredentials = false,
                Port = port,
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
                EnableSsl = true,
            };

            // Retrieve the SMTP username from configuration and validate
            var smtpUsername = _configuration["Smtp:Username"];
            if (string.IsNullOrEmpty(smtpUsername))
            {
                throw new InvalidOperationException("SMTP username is not configured.");
            }

            // Create and configure the MailMessage
            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            // Send the email asynchronously
            return smtpClient.SendMailAsync(mailMessage);
        }
    }
}
