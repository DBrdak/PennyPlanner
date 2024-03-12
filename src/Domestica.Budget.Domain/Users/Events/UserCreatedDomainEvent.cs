using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent
    {
    }
}
