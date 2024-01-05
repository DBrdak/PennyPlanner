using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.TransactionEntities;
using CommonAbstractions.DB.Messaging;

namespace Budgetify.Application.TransactionEntities.AddTransactionEntity
{
    public sealed record AddTransactionEntityCommand(TransactionEntityName Name, TransactionEntityType Type) : ICommand
    {
    }
}
