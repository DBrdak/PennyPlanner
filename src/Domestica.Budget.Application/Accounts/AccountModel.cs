using System.Text.Json.Serialization;
using Domestica.Budget.Application.Accounts.AddAccount;
using Domestica.Budget.Application.Shared.Models;
using Domestica.Budget.Application.Transactions;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;

namespace Domestica.Budget.Application.Accounts
{
    public sealed record AccountModel
    {
        public string AccountId { get; init; }
        public string Name { get; init; }
        public MoneyModel Balance { get; init; }
        public IEnumerable<TransactionModel> Transactions { get; init; }
        public string AccountType { get; init; }

        private AccountModel(string accountId, string name, MoneyModel balance, IEnumerable<TransactionModel> transactions, AccountType accountType)
        {
            AccountId = accountId;
            Name = name;
            Balance = balance;
            Transactions = transactions;
            AccountType = accountType.Value;
        }

        [JsonConstructor]
        private AccountModel()
        {
        }

        internal static AccountModel FromDomainObject(Account account)
        {
            AccountType accountType;

            if (account is TransactionalAccount)
            {
                accountType = AddAccount.AccountType.Transactional;
            }
            else if (account is SavingsAccount)
            {
                accountType = AddAccount.AccountType.Savings;
            }
            else
            {
                throw new ArgumentException("Unknown account type");
            }

            var transactions = account.Transactions
                .Select(TransactionModel.FromDomainObject)
                .OrderByDescending(t => t.TransactionDateUtc);

            var balance = MoneyModel.FromDomainObject(account.Balance);

            return new AccountModel(account.Id.Value.ToString(), account.Name.Value, balance, transactions, accountType);
        }

    }
}
