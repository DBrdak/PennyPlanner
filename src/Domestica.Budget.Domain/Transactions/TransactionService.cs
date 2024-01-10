using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;

namespace Domestica.Budget.Domain.Transactions
{
    public static class TransactionService
    {
        public static void CreateIncomingTransaction(
            Money.DB.Money transactionAmount,
            Account destinationAccount,
            TransactionSender sender,
            IncomingTransactionCategory category)
        {
            var transaction = Transaction.CreateIncome(transactionAmount, destinationAccount, sender, category);

            destinationAccount.AddTransaction(transaction);
            sender.AddTransaction(transaction);
        }

        public static void CreateOutgoingTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            TransactionRecipient recipient,
            OutgoingTransactionCategory category)
        {
            var transaction = Transaction.CreateOutcome(transactionAmount, sourceAccount, recipient, category);

            sourceAccount.AddTransaction(transaction);
            recipient.AddTransaction(transaction);
        }

        public static void CreateInternalTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            Account destinationAccount)
        {
            var transactions = new
            {
                source = Transaction.CreateInternalOutcome(transactionAmount, sourceAccount, destinationAccount),
                destination = Transaction.CreateInternalIncome(transactionAmount, destinationAccount, sourceAccount)
            };

            sourceAccount.AddTransaction(transactions.source);
            destinationAccount.AddTransaction(transactions.destination);
        }

        internal static void CreatePrivateTransaction(
            Money.DB.Money transactionAmount,
            Account account)
        {
            switch (transactionAmount.Amount > 0)
            {
                case true:
                    var equalizingIncomeTransaction = Transaction.CreatePrivateIncome(transactionAmount, account);
                    account.AddTransaction(equalizingIncomeTransaction);
                    break;
                case false:
                    var equalizingOutcomeTransaction = Transaction.CreatePrivateOutcome(transactionAmount, account);
                    account.AddTransaction(equalizingOutcomeTransaction);
                    break;
            }
        }
    }
}
