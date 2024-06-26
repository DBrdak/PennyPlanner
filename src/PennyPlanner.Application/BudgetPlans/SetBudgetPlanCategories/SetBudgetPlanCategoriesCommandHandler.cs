﻿using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Money.DB;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Application.TransactionCategories.AddTransactionCategory;
using PennyPlanner.Domain.BudgetPlans;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.BudgetPlans.SetBudgetPlanCategories
{
    internal sealed class SetBudgetPlanCategoriesCommandHandler : ICommandHandler<SetBudgetPlanCategoriesCommand, BudgetPlan>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly ITransactionCategoryRepository _categoryRepository;

        public SetBudgetPlanCategoriesCommandHandler(IBudgetPlanRepository budgetPlanRepository, IUnitOfWork unitOfWork, ITransactionCategoryRepository categoryRepository, IUserContext userContext)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _userContext = userContext;
        }

        public async Task<Result<BudgetPlan>> Handle(SetBudgetPlanCategoriesCommand request, CancellationToken cancellationToken)
        {
            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(request.BudgetPlanForDate, cancellationToken);

            if (budgetPlan is null)
            {
                budgetPlan = BudgetPlan.CreateForMonth(request.BudgetPlanForDate, new UserId(_userContext.IdentityId));
                await _budgetPlanRepository.AddAsync(budgetPlan, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var currency = Currency.FromCode(_userContext.UserCurrencyCode);

            foreach (var budgetedTransactionCategoryValues in request.BudgetedTransactionCategoryValues)
            {
                var category = await GetOrCreateCategory(
                    new (budgetedTransactionCategoryValues.CategoryValue), 
                    TransactionCategoryType.FromString(budgetedTransactionCategoryValues.CategoryType), 
                    cancellationToken);

                budgetPlan.SetBudgetForCategory(
                    category!,
                    new (budgetedTransactionCategoryValues.BudgetedAmount, currency));
            }

            _budgetPlanRepository.Update(budgetPlan);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(budgetPlan);
        }

        private async Task<TransactionCategory?> GetOrCreateCategory(TransactionCategoryValue categoryValue, TransactionCategoryType categoryType, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByValueAsync<TransactionCategory>(
                categoryValue,
                cancellationToken) ??
                           CreateCategory(categoryValue, categoryType);
        }

        private TransactionCategory CreateCategory(TransactionCategoryValue value, TransactionCategoryType type)
        {
            if (type == TransactionCategoryType.Income)
            {
                return new IncomeTransactionCategory(value, new UserId(_userContext.IdentityId));
            }

            if (type == TransactionCategoryType.Outcome)
            {
                return new OutcomeTransactionCategory(value, new UserId(_userContext.IdentityId));
            }

            return null;
        }
    }
}
