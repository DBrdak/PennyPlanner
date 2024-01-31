using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.Transactions;
using FluentValidation;
using Money.DB;

namespace Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories
{
    public sealed class SetBudgetPlanCategoriesCommandValidator : AbstractValidator<SetBudgetPlanCategoriesCommand>
    {
        public SetBudgetPlanCategoriesCommandValidator()
        {
            RuleForEach(x => x.BudgetedTransactionCategoryValues)
                .Must(values => TransactionCategory.All.Any(category => category.Value == values.Category))
                .WithMessage("Invalid transaction category")
                .Must(values => values.BudgetedAmount > 0)
                .WithMessage("Budgeted amount must be greater than 0");
        }
    }
}
