using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Transactions.DomainEvents
{
    public sealed record TransactionDeletedDomainEvent(Transaction DeletedTransaction) : IDomainEvent
    {
    }
}
