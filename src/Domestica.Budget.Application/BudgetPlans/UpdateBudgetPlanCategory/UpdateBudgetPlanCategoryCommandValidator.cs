using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.Transactions;
using FluentValidation;

namespace Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed class UpdateBudgetPlanCategoryCommandValidator : AbstractValidator<UpdateBudgetPlanCategoryCommand>
    {
        public UpdateBudgetPlanCategoryCommandValidator()
        {
            RuleFor(x => x.Category)
                .Must(
                    category => TransactionCategory.All.Any(
                        transactionCategory => transactionCategory.Value == category.Value))
                .WithMessage("Invalid transaction category");

            RuleFor(x => x)
                .Must(x => x.NewBudgetedAmount is not null || x.IsBudgetToReset)
                .WithMessage("Specify whether you want to reset category or update budgeted amount");
        }
    }
}
