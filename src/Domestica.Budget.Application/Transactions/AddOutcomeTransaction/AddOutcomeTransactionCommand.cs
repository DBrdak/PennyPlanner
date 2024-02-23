using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    public sealed record AddOutcomeTransactionCommand(
        string SourceAccountId,
        string RecipientName,
        decimal TransactionAmount,
        string CategoryValue,
        string SubcategoryValue,
        DateTime TransactionDateTime) : ICommand<Transaction>
    {
    }
}
