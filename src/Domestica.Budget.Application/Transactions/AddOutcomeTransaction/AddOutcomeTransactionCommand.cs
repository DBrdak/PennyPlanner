using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    public sealed record AddOutcomeTransactionCommand(
        string SourceAccountId,
        string RecipientId,
        decimal TransactionAmount,
        string Category,
        DateTime TransactionDateTime) : ICommand<Transaction>
    {
    }
}
