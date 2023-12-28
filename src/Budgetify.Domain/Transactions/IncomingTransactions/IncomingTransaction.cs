using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Shared.TransactionCategories;

namespace Budgetify.Domain.Transactions.IncomingTransactions
{
    public sealed class IncomingTransaction : Transaction
    {
        public string DestinationAccountId { get; private set; }
        public string? SenderId { get; private set; }
        public IncomingTransactionCategory Category { get; private set; }

        private IncomingTransaction(
            Money.DB.Money transactionAmount,
            string destinationAccountId,
            string? senderId,
            IncomingTransactionCategory category) : base(transactionAmount)
        {
            DestinationAccountId = destinationAccountId;
            SenderId = senderId;
            Category = category;
        }

        internal static IncomingTransaction Create(
            Money.DB.Money transactionAmount,
            string destinationAccountId,
            string? senderId,
            IncomingTransactionCategory category)
        {
            return new IncomingTransaction(transactionAmount, destinationAccountId, senderId, category);
        }
    }
}
