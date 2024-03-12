using Exceptions.DB;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities.TransactionRecipients;
using PennyPlanner.Domain.TransactionEntities.TransactionSenders;
using PennyPlanner.Domain.TransactionSubcategories;

namespace PennyPlanner.Domain.Transactions
{
    public static class TransactionService
    {
        public static Transaction CreateIncomingTransaction(
            Money.DB.Money transactionAmount,
            Account destinationAccount,
            TransactionSender sender,
            IncomeTransactionCategory category,
            TransactionSubcategory subcategory,
            DateTime transactionDateTime)
        {
            ValidateSubcategory(category, subcategory);

            var transaction = Transaction.CreateIncome(transactionAmount, destinationAccount, sender, subcategory, transactionDateTime);

            destinationAccount.AddTransaction(transaction);
            sender.AddTransaction(transaction);

            return transaction;
        }

        public static Transaction CreateOutgoingTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            TransactionRecipient recipient,
            OutcomeTransactionCategory category,
            TransactionSubcategory subcategory,
            DateTime transactionDateTime)
        {
            ValidateSubcategory(category, subcategory);

            var transaction = Transaction.CreateOutcome(transactionAmount, sourceAccount, recipient, subcategory, transactionDateTime);

            sourceAccount.AddTransaction(transaction);
            recipient.AddTransaction(transaction);

            return transaction;
        }

        public static Transaction[] CreateInternalTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            Account destinationAccount,
            DateTime transactionDateTime) 
        {
            var transactions = new
            {
                source = Transaction.CreateInternalOutcome(transactionAmount, sourceAccount, destinationAccount, transactionDateTime),
                destination = Transaction.CreateInternalIncome(transactionAmount, destinationAccount, sourceAccount, transactionDateTime)
            };

            sourceAccount.AddTransaction(transactions.source);
            destinationAccount.AddTransaction(transactions.destination);

            return new []
            {
                transactions.source, 
                transactions.destination
            };
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

        private static void ValidateSubcategory(
            TransactionCategory category,
            TransactionSubcategory subcategory)
        {
            var subcategoryInCategory = category.Subcategories.Any(sc => sc.Id == subcategory.Id);

            if (subcategoryInCategory is false)
            {
                throw new DomainException<Transaction>($"Subcategory {subcategory.Value.Value} doesn't exist in category {category.Value.Value}");
            }
        }
    }
}
