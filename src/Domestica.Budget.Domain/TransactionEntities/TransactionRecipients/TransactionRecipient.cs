using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Domain.TransactionEntities.TransactionRecipients
{
    public sealed class TransactionRecipient : TransactionEntity
    {
        private TransactionRecipient()
        { }

        public TransactionRecipient(TransactionEntityName name, UserId userId) : base(name, userId)
        {
        }
        
    }
}
