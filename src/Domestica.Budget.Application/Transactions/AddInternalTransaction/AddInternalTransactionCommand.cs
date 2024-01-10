using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    public sealed record AddInternalTransactionCommand(AccountId FromAccountId, AccountId ToAccountId, Money.DB.Money TransactionAmount) : ICommand
    {
    }
}
