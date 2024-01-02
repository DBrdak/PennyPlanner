using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Budgetify.Domain.Shared.TransactionCategories;
using Budgetify.Domain.TransactionEntities.TransactionRecipients;
#pragma warning disable CS8618

namespace Budgetify.Domain.Transactions.OugoingTransactions
{
    public sealed class OutgoingTransaction : Transaction
    {
        public Account SourceAccount { get; private set; }
        public string SourceAccountId { get; private set; }
        public TransactionRecipient? Recipient { get; private set; }
        public string? RecipientId { get; private set; }
        public Account? InternalDestinationAccount { get; private set; }
        public string? InternalDestinationAccountId { get; private set; }
        public OutgoingTransactionCategory Category { get; private set; }

        private OutgoingTransaction()
        { }

        private OutgoingTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            TransactionRecipient? recipient,
            Account? internalDestinationAccount,
            OutgoingTransactionCategory category) : base(transactionAmount)
        {
            SourceAccount = sourceAccount;
            SourceAccountId = sourceAccount.Id;
            Recipient = recipient;
            RecipientId = recipient?.Id;
            InternalDestinationAccount = internalDestinationAccount;
            InternalDestinationAccountId = internalDestinationAccount?.Id;
            Category = category;
        }

        internal static OutgoingTransaction CreateInternal(
            Money.DB.Money transactionAmount,
            Account sourceAccountId,
            Account internalDestinationAccountId)
        {
            return new (transactionAmount, sourceAccountId, null, internalDestinationAccountId, OutgoingTransactionCategory.Internal);
        }

        internal static OutgoingTransaction Create(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            TransactionRecipient recipient,
            OutgoingTransactionCategory category)
        {
            return new (transactionAmount, sourceAccount, recipient, null, category);
        }
    }
}
