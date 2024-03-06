using Domestica.Budget.Application.Abstractions.Email;

namespace Domestica.Budget.Infrastructure.Email
{
    public sealed class EmailService : IEmailService
    {
        public async Task SendAsync(Domain.Users.Email recipient, string subject, string body)
        {
            await Task.Delay(300);
        }
    }
}
