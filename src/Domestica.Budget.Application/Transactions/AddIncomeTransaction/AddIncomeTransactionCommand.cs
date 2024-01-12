using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    public sealed record AddIncomeTransactionCommand(
        AccountId DestinationAccountId,
        TransactionEntityId SenderId,
        Money.DB.Money TransactionAmount,
        IncomingTransactionCategory Category) : ICommand<Transaction>
    {
    }
}
