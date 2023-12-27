using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Transactions
{
    public sealed class IncomingTransaction : Transaction
    {
        public string DestinationAccountId { get; private set; }
        public string? SenderId { get; private set; }

        internal IncomingTransaction(Money.DB.Money transactionAmount, string destinationAccountId, string? senderId) : base(transactionAmount)
        {
            DestinationAccountId = destinationAccountId;
            SenderId = senderId;
        }
    }
}
