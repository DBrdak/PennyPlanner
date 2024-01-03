﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using CommonAbstractions.DB.Messaging;

namespace Budgetify.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery() : IQuery<List<Account>>
    {
    }
}
