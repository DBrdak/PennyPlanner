using Budgetify.Domain.Accounts;
using CommonAbstractions.DB.Messaging;

namespace Budgetify.Application.Transactions.AddInternalTransaction
{
    public sealed record AddInternalTransactionCommand(AccountId FromAccountId, AccountId ToAccountId, Money.DB.Money TransactionAmount) : ICommand
    {
    }
}
