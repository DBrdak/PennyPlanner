using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Budgetify.Domain.TransactionEntities;
using Budgetify.Domain.Transactions;
using CommonAbstractions.DB.Messaging;

namespace Budgetify.Application.Transactions.AddIncomeTransaction
{
    public sealed record AddIncomeTransactionCommand(
        AccountId DestinationAccountId,
        TransactionEntityId SenderId,
        Money.DB.Money TransactionAmount,
        IncomingTransactionCategory Category) : ICommand
    {
    }
}
