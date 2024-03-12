using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Accounts;

namespace PennyPlanner.Application.Accounts.AddAccount
{
    public sealed record AddAccountCommand(NewAccountData NewAccountData) : ICommand<Account>;
}
