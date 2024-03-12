using Microsoft.Extensions.Options;
using PennyPlanner.Application.Abstractions.Email;
using Responses.DB;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PennyPlanner.Infrastructure.Email
{
    public sealed class EmailService : IEmailService
    {
        private readonly EmailProviderOptions _emailProviderProviderOptions;

        public EmailService(IOptions<EmailProviderOptions> options)
        {
            _emailProviderProviderOptions = options.Value;
        }

        public async Task<Result> SendAsync(Domain.Users.Email recipient, string subject, string plainBody, string htmlBody)
        {
            return Result.Success();
            var client = new SendGridClient(_emailProviderProviderOptions.Key);

            subject = "Sending with SendGrid is Fun";
            plainBody = "and easy to do anywhere, even with C#";
            htmlBody = "<strong>and easy to do anywhere, even with C#</strong>";

            var message = new SendGridMessage
            {
                From = GetSender(),
                Subject = subject,
                PlainTextContent = plainBody,
                HtmlContent = htmlBody
            };

            message.AddTo(new EmailAddress(recipient.Value));

            var response = await client.SendEmailAsync(message);

            var responseMessage = await response.Body.ReadAsStringAsync();

            return response.IsSuccessStatusCode ?
                Result.Success() :
                Result.Failure(Error.TaskFailed($"Problem while sending email to {recipient.Value}, {responseMessage}"));
        }

        private EmailAddress GetSender() => new("tontav8@gmail.com", "Example User");
    }
}
