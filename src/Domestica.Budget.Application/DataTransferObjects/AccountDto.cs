using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Application.Accounts.AddAccount;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Newtonsoft.Json;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record AccountDto
    {
        public string AccountId { get; init; }
        public string Name { get; init; }
        public MoneyDto Balance { get; init; }
        public IEnumerable<TransactionDto> Transactions { get; init; }
        public string AccountType { get; init; }

        private AccountDto(string accountId, string name, MoneyDto balance, IEnumerable<TransactionDto> transactions, AccountType accountType)
        {
            AccountId = accountId;
            Name = name;
            Balance = balance;
            Transactions = transactions;
            AccountType = accountType.Value;
        }

        [JsonConstructor]
        private AccountDto()
        {
        }

        internal static AccountDto FromDomainObject(Account account)
        {
            AccountType accountType;

            if (account is TransactionalAccount)
            {
                accountType = Accounts.AddAccount.AccountType.Transactional;
            }
            else if (account is SavingsAccount)
            {
                accountType = Accounts.AddAccount.AccountType.Savings;
            }
            else
            {
                throw new ArgumentException("Unknown account type");
            }

            var transactions = account.Transactions.Select(TransactionDto.FromDomainObject);

            var balance = MoneyDto.FromDomainObject(account.Balance);

            return new AccountDto(account.Id.Value.ToString(), account.Name.Value, balance, transactions, accountType);
        }

    }
}
