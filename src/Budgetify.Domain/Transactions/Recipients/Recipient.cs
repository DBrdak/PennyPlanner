using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.Transactions.Recipients
{
    public sealed class Recipient : Entity
    {
        public RecipientName Name { get; private set; }
        public RecipientCategory Category { get; private set; }

        public Recipient(RecipientName name, RecipientCategory category) : base()
        {
            Name = name;
            Category = category;
        }
    }
}
