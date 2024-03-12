using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Application.Transactions.AddOutcomeTransaction
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
