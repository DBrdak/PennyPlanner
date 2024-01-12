using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;
using Currency = Money.DB.Currency;
using Transaction = Domestica.Budget.Domain.Transactions.Transaction;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.Accounts
{
    public abstract class Account : Entity<AccountId>
    {
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        private readonly List<Transaction> _transactions;
        public AccountName Name { get; private set; }
        [NotMapped]
        public Money.DB.Money Balance => new(_transactions.Sum(transaction => transaction.TransactionAmount.Amount), Currency);
        public Currency Currency { get; private set; }
        public bool IsActive { get; private set; }

        [JsonConstructor]
        protected Account()
        { }

        protected Account(AccountName name, Currency currency, decimal initialBalance) : base(new AccountId())
        {
            Name = name;
            Currency = currency;
            _transactions = new();
            IsActive = true;

            TransactionService.CreatePrivateTransaction(new (initialBalance, currency), this);
        }

        public void UpdateAccount(AccountName name, global::Money.DB.Money balance)
        {
            if (!IsActive)
            {
                throw new DomainException<Account>("Cannot update inactive account");
            }

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
            if (!IsActive)
            {
                throw new DomainException<Account>("Cannot add transaction to inactive account");
            }

            _transactions.Add(transaction);
            //Balance += transaction.TransactionAmount;
        }

        private void ChangeAccountName(AccountName newName)
        {
            Name = newName;
        }

        private void AdjustAccountBalance(global::Money.DB.Money newBalance)
        {
            if (newBalance.Currency != Balance.Currency)
            {
                Currency = newBalance.Currency;
            }

            var difference = newBalance - Balance;

            TransactionService.CreatePrivateTransaction(difference, this);
        }

        public void DeactivateAccount() => IsActive = false;
    }
}
