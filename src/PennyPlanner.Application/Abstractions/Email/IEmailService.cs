using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task<Result> SendWelcomeEmailAsync(Domain.Users.Email recipient, Username username, UserId userId);
    }
}
