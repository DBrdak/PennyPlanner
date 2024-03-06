using CommonAbstractions.DB.Messaging;

namespace Domestica.Budget.Application.Users.LogInUser
{
    public sealed record LogInUserCommand(string Email,string Password) : ICommand<AccessToken>
    {
    }
}
