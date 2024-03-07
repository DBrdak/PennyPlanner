using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Email,
        string Password,
        string Currency) : ICommand<User>
    {
    }
}
