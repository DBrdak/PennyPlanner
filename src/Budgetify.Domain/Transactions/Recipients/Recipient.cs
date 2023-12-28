using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.Transactions.Recipients
{
    public sealed class Recipient(RecipientName name) : Entity;
}
