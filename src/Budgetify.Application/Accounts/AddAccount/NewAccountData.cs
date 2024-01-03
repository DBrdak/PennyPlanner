using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Money.DB;

namespace Budgetify.Application.Accounts.AddAccount
{
    public sealed record NewAccountData(AccountType Type, AccountName Name, Currency Currency, decimal InitialBalance)
    {
    }
}
