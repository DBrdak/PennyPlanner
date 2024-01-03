using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;

namespace Budgetify.Application.Accounts.UpdateAccount
{
    public sealed record UpdateAccountCommand(AccountUpdateData AccountUpdateData) : ICommand
    {
    }
}
