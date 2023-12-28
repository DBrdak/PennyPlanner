using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Shared.TransactionCategories;

namespace Budgetify.Domain.Transactions.OugoingTransactions
{
    public sealed class OutgoingTransaction : Transaction
    {
        public string SourceAccountId { get; private set; }
        public string? RecipientId { get; private set; }
        public OutgoingTransactionCategory Category { get; private set; }

        private OutgoingTransaction(
            Money.DB.Money transactionAmount,
            string sourceAccountId,
            string? recipientId,
            OutgoingTransactionCategory category) : base(transactionAmount)
        {
            SourceAccountId = sourceAccountId;
            RecipientId = recipientId;
            Category = category;
        }

        internal static OutgoingTransaction Create(
            Money.DB.Money transactionAmount,
            string sourceAccountId,
            string? recipientId,
            OutgoingTransactionCategory category)
        {
            return new OutgoingTransaction(transactionAmount, sourceAccountId, recipientId, category);
        }
    }
}
