using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.Transactions.DomainEvents
{
    public sealed record TransactionDeletedDomainEvent(Transaction DeletedTransaction) : IDomainEvent
    {
    }
}
