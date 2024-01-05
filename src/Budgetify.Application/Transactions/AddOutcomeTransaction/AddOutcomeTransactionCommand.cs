using Budgetify.Domain.Accounts;
using Budgetify.Domain.TransactionEntities;
using Budgetify.Domain.Transactions;
using CommonAbstractions.DB.Messaging;

namespace Budgetify.Application.Transactions.AddOutcomeTransaction
{
    public sealed record AddOutcomeTransactionCommand(
        AccountId SourceAccountId,
        TransactionEntityId RecipientId,
        Money.DB.Money TransactionAmount,
        OutgoingTransactionCategory Category) : ICommand
    {
    }
}
