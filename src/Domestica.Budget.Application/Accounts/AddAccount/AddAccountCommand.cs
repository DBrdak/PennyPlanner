using CommonAbstractions.DB.Messaging;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    public sealed record AddAccountCommand(NewAccountData NewAccountData) : ICommand;
}
