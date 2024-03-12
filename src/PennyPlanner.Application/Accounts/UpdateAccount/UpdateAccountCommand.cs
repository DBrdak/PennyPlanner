using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Accounts;

namespace PennyPlanner.Application.Accounts.UpdateAccount
{
    public sealed record UpdateAccountCommand(AccountUpdateData AccountUpdateData) : ICommand<Account>
    {
    }
}
