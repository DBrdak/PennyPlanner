using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Application.Transactions.RemoveTransaction
{
    public sealed record RemoveTransactionCommand(string TransactionId) : ICommand<Transaction>
    {
    }
}
