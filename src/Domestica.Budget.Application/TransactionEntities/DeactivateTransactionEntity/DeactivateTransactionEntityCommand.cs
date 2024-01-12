using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Application.TransactionEntities.DeactivateTransactionEntity
{
    public sealed record DeactivateTransactionEntityCommand(TransactionEntityId TransactionEntityId) : ICommand<TransactionEntity>
    {
    }
}
