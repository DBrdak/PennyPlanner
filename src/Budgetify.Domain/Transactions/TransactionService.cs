using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Shared.TransactionCategories;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;

namespace Budgetify.Domain.Transactions
{
    public static class TransactionService
    {
        public static IncomingTransaction CreateIncomingTransaction(
            Money.DB.Money transactionAmount,
            string destinationAccountId,
            string senderId,
            IncomingTransactionCategory category)
        {
            return IncomingTransaction.Create(transactionAmount, destinationAccountId, senderId, category);
        }

        public static OutgoingTransaction CreateOutgoingTransaction(
            Money.DB.Money transactionAmount,
            string sourceAccountId,
            string recipientId,
            OutgoingTransactionCategory category)
        {
            return OutgoingTransaction.Create(transactionAmount, sourceAccountId, recipientId, category);
        }

        public static Transaction[] CreateInternalTransaction(
            Money.DB.Money transactionAmount,
            string sourceAccountId,
            string destinationAccountId)
        {
            return new Transaction[]
            {
                CreateOutgoingTransaction(transactionAmount, sourceAccountId, destinationAccountId, OutgoingTransactionCategory.Internal),
                CreateIncomingTransaction(transactionAmount, destinationAccountId, sourceAccountId, IncomingTransactionCategory.Internal)
            };
        }
    }
}
