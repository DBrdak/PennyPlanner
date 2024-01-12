using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Accounts.DeactivateAccount
{
    public sealed record DeactivateAccountCommand(AccountId AccountId) : ICommand<Account>
    {
    }
}
