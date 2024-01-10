using CommonAbstractions.DB.Messaging;

namespace Domestica.Budget.Application.Accounts.UpdateAccount
{
    public sealed record UpdateAccountCommand(AccountUpdateData AccountUpdateData) : ICommand
    {
    }
}
