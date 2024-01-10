using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.Transactions
{
    public class Transaction : Entity<TransactionId>
    {
        [JsonIgnore]
        public Account Account { get; protected set; }
        public AccountId AccountId { get; protected set; }
        public Account? FromAccount { get; protected set; }
        public AccountId? FromAccountId { get; protected set; }
        public Account? ToAccount { get; protected set; }
        public AccountId? ToAccountId { get; protected set; }
        public TransactionSender? Sender { get; protected set; }
        public TransactionEntityId? SenderId { get; protected set; }
        public TransactionRecipient? Recipient { get; protected set; }
        public TransactionEntityId? RecipientId { get; protected set; }
        public Money.DB.Money TransactionAmount { get; private set; }
        public TransactionCategory Category { get; private set; }
        public DateTime TransactionDateUtc { get; private set; }

        protected Transaction()
        { }

        private Transaction(
            Account account,
            Account? fromAccount,
            Account? toAccount,
            TransactionSender? sender,
            TransactionRecipient? recipient,
            Money.DB.Money transactionAmount,
            TransactionCategory category) : base(new TransactionId())
        {
            Account = account;
            AccountId = account.Id;
            FromAccount = fromAccount;
            FromAccountId = fromAccount?.Id;
            ToAccount = toAccount;
            ToAccountId = toAccount?.Id;
            Sender = sender;
            SenderId = sender?.Id;
            Recipient = recipient;
            RecipientId = recipient?.Id;
            TransactionAmount = transactionAmount;
            TransactionDateUtc = DateTime.UtcNow;
            Category = category;
        }

        internal static Transaction CreateIncome(
            Money.DB.Money transactionAmount,
            Account toAccount,
            TransactionSender sender,
            IncomingTransactionCategory category)
        {
            return new(toAccount, null, null, sender, null, transactionAmount, category);
        }

        internal static Transaction CreateOutcome(
            Money.DB.Money transactionAmount,
            Account fromAccount,
            TransactionRecipient recipient,
            OutgoingTransactionCategory category)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(fromAccount, null, null, null, recipient, -transactionAmount, category);
            }

            return new(fromAccount, null, null, null, recipient, transactionAmount, category);
        }

        internal static Transaction CreateInternalIncome(
            Money.DB.Money transactionAmount,
            Account toAccount,
            Account fromAccount)
        {
            return new(toAccount, fromAccount, null, null, null, transactionAmount, IncomingTransactionCategory.Internal);
        }

        internal static Transaction CreateInternalOutcome(
            Money.DB.Money transactionAmount,
            Account fromAccount,
            Account toAccount)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(fromAccount, null, toAccount, null, null, -transactionAmount, OutgoingTransactionCategory.Internal);
            }

            return new(fromAccount, null, toAccount, null, null, transactionAmount, OutgoingTransactionCategory.Internal);
        }

        internal static Transaction CreatePrivateIncome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            return new(account, null, null, null, null, transactionAmount, IncomingTransactionCategory.Private);
        }

        internal static Transaction CreatePrivateOutcome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(account, null, null, null, null, -transactionAmount, OutgoingTransactionCategory.Private);
            }

            return new(account, null, null, null, null, transactionAmount, OutgoingTransactionCategory.Private);
        }
    }
}
