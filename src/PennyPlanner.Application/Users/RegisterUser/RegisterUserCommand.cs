using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Username,
        string Email,
        string Password,
        string Currency) : ICommand<User>
    {
    }
}
