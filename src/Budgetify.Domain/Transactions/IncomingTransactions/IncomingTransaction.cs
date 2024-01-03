using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Budgetify.Domain.TransactionEntities;
using Budgetify.Domain.TransactionEntities.TransactionSenders;
#pragma warning disable CS8618

namespace Budgetify.Domain.Transactions.IncomingTransactions
{
    public sealed class IncomingTransaction : Transaction
    {
        public TransactionSender? Sender { get; private set; }
        public TransactionEntityId? SenderId { get; private set; }
        public Account? InternalSourceAccount { get; private set; }
        public AccountId? InternalSourceAccountId { get; private set; }
        public IncomingTransactionCategory Category { get; private set; }

        private IncomingTransaction()
        { }

        private IncomingTransaction(
            Money.DB.Money transactionAmount,
            Account destinationAccount,
            TransactionSender? sender,
            Account? internalSourceAccount,
            IncomingTransactionCategory category) : base(transactionAmount)
        {
            Account = destinationAccount;
            AccountId = destinationAccount.Id;
            Sender = sender;
            SenderId = sender?.Id;
            InternalSourceAccount = internalSourceAccount;
            InternalSourceAccountId = internalSourceAccount?.Id;
            Category = category;
        }

        internal static IncomingTransaction CreateInternal(
            Money.DB.Money transactionAmount,
            Account destinationAccount,
            Account internalSourceAccount)
        {
            return new (transactionAmount, destinationAccount, null, internalSourceAccount, IncomingTransactionCategory.Internal);
        }

        internal static IncomingTransaction CreateEqualizing(
            Money.DB.Money transactionAmount,
            Account account)
        {
            return new(transactionAmount, account, null, null, IncomingTransactionCategory.Internal);
        }

        internal static IncomingTransaction Create(
            Money.DB.Money transactionAmount,
            Account destinationAccount,
            TransactionSender sender,
            IncomingTransactionCategory category)
        {
            return new (transactionAmount, destinationAccount, sender, null, category);
        }
    }
}
