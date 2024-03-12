using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Accounts;

namespace PennyPlanner.Application.Accounts.RemoveAccount
{
    public sealed record RemoveAccountCommand(string AccountId) : ICommand<Account>
    {
    }
}
