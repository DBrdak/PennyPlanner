using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PennyPlanner.Application.Abstractions.Email;
using PennyPlanner.Domain.Users;
using PennyPlanner.Infrastructure.Email.EmailDesign;
using Responses.DB;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PennyPlanner.Infrastructure.Email
{
    public sealed class EmailService : IEmailService
    {
        private readonly EmailProviderOptions _emailProviderProviderOptions;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IEmailVerificationService _emailVerificationService;

        public EmailService(IOptions<EmailProviderOptions> options, IHttpContextAccessor httpContext, IEmailVerificationService emailVerificationService)
        {
            _httpContext = httpContext;
            _emailVerificationService = emailVerificationService;
            _emailProviderProviderOptions = options.Value;
        }

        public async Task<Result> SendWelcomeEmailAsync(Domain.Users.Email recipient, Username username, UserId userId)
        {
            var client = new SendGridClient(_emailProviderProviderOptions.Key);

            var verificationUrl = GenerateVerificationUrl(recipient, userId);

            var message = new SendGridMessage
            {
                From = GetSender(),
                Subject = EmailSubject.WelcomeSubject.Value,
                PlainTextContent = "",
                HtmlContent = new WelcomeEmailTemplate(username.Value, verificationUrl).GetHtmlTemplate()
            };

            message.AddTo(new EmailAddress(recipient.Value));

            var response = await client.SendEmailAsync(message);

            var responseMessage = await response.Body.ReadAsStringAsync();

            return response.IsSuccessStatusCode ?
                Result.Success() :
                Result.Failure(Error.TaskFailed($"Problem while sending email to {recipient.Value}, {responseMessage}"));
        }

        private string GenerateVerificationUrl(Domain.Users.Email recipient, UserId userId)
        {
            var relativeVerificationUrl = GenerateRelativeUrl(recipient, userId);
            var origin = _httpContext.HttpContext?.Request.Headers["origin"];

            var verificationUrl = $"{origin}/{relativeVerificationUrl}";
            return verificationUrl;
        }

        private string GenerateRelativeUrl(Domain.Users.Email recipient, UserId userId)
        {
            var token = _emailVerificationService.GenerateEmailVerificationToken(userId.Value.ToString());
            var relativeVerificationUrl = $"email-verification/verify?token={token}&email={recipient.Value}";
            return relativeVerificationUrl;
        }

        private EmailAddress GetSender() =>
            new(
                _emailProviderProviderOptions.Sender,
                _emailProviderProviderOptions.Name);
    }
}
