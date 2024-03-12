using CommonAbstractions.DB.Messaging;

namespace PennyPlanner.Application.Users.LogInUser
{
    public sealed record LogInUserCommand(string Email,string Password) : ICommand<AccessToken>
    {
    }
}
