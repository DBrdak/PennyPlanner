using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionEntities;

namespace PennyPlanner.Application.TransactionEntities.UpdateTransactionEntity
{
    public sealed record UpdateTransactionEntityCommand
        (string Id, string NewName) : ICommand<TransactionEntity>
    {
    }
}
