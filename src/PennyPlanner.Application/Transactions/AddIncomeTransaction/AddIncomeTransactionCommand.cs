using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Application.Transactions.AddIncomeTransaction
{
    public sealed record AddIncomeTransactionCommand(
        string DestinationAccountId,
        string SenderName,
        decimal TransactionAmount,
        string CategoryValue,
        string SubcategoryValue,
        DateTime TransactionDateTime) : ICommand<Transaction>
    {
    }
}
