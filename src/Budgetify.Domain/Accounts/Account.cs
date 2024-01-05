using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Budgetify.Domain.Transactions;
using CommonAbstractions.DB.Entities;
using Currency = Money.DB.Currency;
using Money = Money.DB.Money;
using Transaction = Budgetify.Domain.Transactions.Transaction;

#pragma warning disable CS8618

namespace Budgetify.Domain.Accounts
{
    public abstract class Account : Entity<AccountId>
    {
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        private readonly List<Transaction> _transactions;
        public AccountName Name { get; private set; }
        public global::Money.DB.Money Balance { get; private set; }

        [JsonConstructor]
        protected Account()
        { }

        protected Account(AccountName name, Currency currency, decimal initialBalance) : base(new AccountId())
        {
            Name = name;
            Balance = new global::Money.DB.Money(0, currency);
            _transactions = new();

            TransactionService.CreatePrivateTransaction(new (initialBalance, currency), this);
        }

        public void UpdateAccount(AccountName name, global::Money.DB.Money balance)
        {
            if(balance != Balance)
            {
                AdjustAccountBalance(balance);
            }

            if(name.Value != Name.Value)
            {
                ChangeAccountName(name);
            }
        }

        internal void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            Balance += transaction.TransactionAmount;
        }

        private void ChangeAccountName(AccountName newName)
        {
            Name = newName;
        }

        private void AdjustAccountBalance(global::Money.DB.Money newBalance)
        {
            if (newBalance.Currency != Balance.Currency)
            {
                Balance = new(Balance.Amount, newBalance.Currency);
            }

            var difference = newBalance - Balance;

            TransactionService.CreatePrivateTransaction(difference, this);
        }
    }
}
