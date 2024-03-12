using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Application.Transactions.AddInternalTransaction
{
    public sealed record AddInternalTransactionCommand(
        string FromAccountId,
        string ToAccountId,
        decimal TransactionAmount,
        DateTime TransactionDateTime) : ICommand<Transaction[]>
    {
    }
}
