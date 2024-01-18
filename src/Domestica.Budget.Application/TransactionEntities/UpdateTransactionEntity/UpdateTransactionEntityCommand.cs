using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Application.TransactionEntities.UpdateTransactionEntity
{
    public sealed record UpdateTransactionEntityCommand
        (string Id, string NewName) : ICommand<TransactionEntity>
    {
    }
}
