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
                .Must(values => values.BudgetedAmount.Amount > 0)
                .WithMessage("Budgeted amount must be greater than 0")
                .Must(values => Currency.All.Any(currency => currency.Code == values.BudgetedAmount.Currency))
                .WithMessage("Invalid currency");
        }
    }
}
