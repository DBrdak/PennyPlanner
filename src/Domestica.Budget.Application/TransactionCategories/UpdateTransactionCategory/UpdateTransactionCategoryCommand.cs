using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Application.TransactionCategories.UpdateTransactionCategory
{
    public sealed record UpdateTransactionCategoryCommand(string TransactionCategoryId, string NewValue) : ICommand<TransactionCategory>
    {
    }
}
