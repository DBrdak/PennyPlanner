using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;

namespace Budgetify.Application.Accounts.UpdateAccount
{
    public sealed record AccountUpdateData(AccountId AccountId, AccountName Name, Money.DB.Money Balance)
    {
    }
}
