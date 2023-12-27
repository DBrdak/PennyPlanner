using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Transactions
{
    public sealed class OutgoingTransaction : Transaction
    {
        public string SourceAccountId { get; private set; }
        public string? RecipientId { get; private set; }

        internal OutgoingTransaction(Money.DB.Money transactionAmount, string sourceAccountId, string? recipientId) : base(transactionAmount)
        {
            SourceAccountId = sourceAccountId; 
            RecipientId = recipientId;
        }
    }
}
