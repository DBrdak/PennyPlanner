using CommonAbstractions.DB.Messaging;

namespace Domestica.Budget.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Email,
        string Password,
        string Currency) : ICommand<UserModel>
    {
    }
}
