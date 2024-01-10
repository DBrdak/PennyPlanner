using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    public sealed record AddOutcomeTransactionCommand(
        AccountId SourceAccountId,
        TransactionEntityId RecipientId,
        Money.DB.Money TransactionAmount,
        OutgoingTransactionCategory Category) : ICommand
    {
    }
}
