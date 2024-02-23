using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    public sealed record AddInternalTransactionCommand(
        string FromAccountId,
        string ToAccountId,
        decimal TransactionAmount,
        DateTime TransactionDateTime) : ICommand<Transaction[]>
    {
    }
}
