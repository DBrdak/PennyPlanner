using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Accounts.RemoveAccount
{
    public sealed record RemoveAccountCommand(string AccountId) : ICommand<Account>
    {
    }
}
