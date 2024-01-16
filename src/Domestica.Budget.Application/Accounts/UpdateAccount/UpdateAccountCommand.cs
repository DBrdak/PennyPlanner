using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Accounts.UpdateAccount
{
    public sealed record UpdateAccountCommand(AccountUpdateData AccountUpdateData) : ICommand<Account>
    {
    }
}
