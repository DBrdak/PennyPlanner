using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    public sealed record AddAccountCommand(NewAccountData NewAccountData) : ICommand<Account>;
}
