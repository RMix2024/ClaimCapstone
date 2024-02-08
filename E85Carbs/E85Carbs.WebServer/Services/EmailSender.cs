using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace E85Carbs.WebServer.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Eurotuner1981@Gmail.com", "Eurotuner1981@Gmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
//static async Task Execute()
//{
//    var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
//    var client = new SendGridClient(apiKey);
//    var from = new EmailAddress("test@example.com", "Example User");
//    var subject = "Sending with SendGrid is Fun";
//    var to = new EmailAddress("test@example.com", "Example User");
//    var plainTextContent = "and easy to do anywhere, even with C#";
//    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//    var response = await client.SendEmailAsync(msg);
//}