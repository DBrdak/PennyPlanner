using Responses.DB;

namespace PennyPlanner.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task<Result> SendAsync(Domain.Users.Email recipient, string subject, string plainBody, string htmlBody);
    }
}
