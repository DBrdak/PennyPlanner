using Budgetify.Domain.TransactionAccounts;
using Budgetify.Domain.TransactionAccounts.BankAccounts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.Transactions
{
    public abstract class Transaction : Entity
    {
        public Money.DB.Money TransactionAmount { get; private set; }
        public DateTime TransactionDateUtc { get; private set; }

        protected Transaction(Money.DB.Money transactionAmount) : base()
        {
            TransactionAmount = transactionAmount;
            TransactionDateUtc = DateTime.UtcNow;
        }

        //TODO: Add DomainEvents

        public static IncomingTransaction CreateIncomingTransaction(
            Money.DB.Money transactionAmount,
            string destinationAccountId,
            string senderId)
        {
            return new IncomingTransaction(transactionAmount, destinationAccountId, senderId);
        }

        public static OutgoingTransaction CreateOutgoingTransaction(
            Money.DB.Money transactionAmount,
            string sourceAccountId,
            string recipientId)
        {
            return new OutgoingTransaction(transactionAmount, sourceAccountId, recipientId);
        }

        public static Transaction[] CreateInternalTransaction(
            Money.DB.Money transactionAmount,
            string sourceAccountId,
            string destinationAccountId)
        {
            return new Transaction[]
            {
                new IncomingTransaction(transactionAmount, destinationAccountId, null),
                new OutgoingTransaction(transactionAmount, sourceAccountId, null)
            };
        }
    }
}
