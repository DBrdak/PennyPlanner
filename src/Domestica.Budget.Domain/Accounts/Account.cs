using System.Text.Json.Serialization;
using Domestica.Budget.Domain.Shared;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.Users;
using Currency = Money.DB.Currency;
using Transaction = Domestica.Budget.Domain.Transactions.Transaction;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.Accounts
{
    public abstract class Account : IdentifiedEntity<AccountId>
    {
        public IReadOnlyCollection<Transaction> Transactions => _transactions.OrderByDescending(t => t.TransactionDateUtc).ToList();
        private readonly List<Transaction> _transactions;
        public AccountName Name { get; private set; }
        public global::Money.DB.Money Balance { get; private set; }

        [JsonConstructor]
        protected Account()
        { }

        protected Account(AccountName name, Currency currency, decimal initialBalance, UserIdentityId userId) : base(userId)
        {
            Name = name;
            _transactions = new();
            Balance = new(initialBalance, currency);

            if (initialBalance != 0)
            {
                TransactionService.CreatePrivateTransaction(new(initialBalance, currency), this);
            }
        }

        public void UpdateAccount(AccountName name, decimal balance)
        {
            if(balance != Balance.Amount)
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

        public void RemoveTransaction(Transaction transaction)
        {
            _transactions.Remove(transaction);
            Balance -= transaction.TransactionAmount;
        }

        private void ChangeAccountName(AccountName newName)
        {
            Name = newName;
        }

        private void AdjustAccountBalance(decimal newBalance)
        {
            var difference = new global::Money.DB.Money(newBalance - Balance.Amount, Balance.Currency);

            TransactionService.CreatePrivateTransaction(difference, this);
        }
    }
}
