﻿using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.TransactionCategories.AddTransactionCategory
{
    internal sealed class AddTransactionCategoryCommandHandler : ICommandHandler<AddTransactionCategoryCommand, TransactionCategory>
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public AddTransactionCategoryCommandHandler(ITransactionCategoryRepository transactionCategoryRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<TransactionCategory>> Handle(AddTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            TransactionCategory transactionCategory;

            if (string.Equals(request.Type, TransactionCategoryType.Income.Value, StringComparison.CurrentCultureIgnoreCase))
            {
                transactionCategory = new IncomeTransactionCategory(new(request.Value), new UserId(_userContext.IdentityId));
            }
            else if (string.Equals(request.Type, TransactionCategoryType.Outcome.Value, StringComparison.CurrentCultureIgnoreCase))
            {
                transactionCategory = new OutcomeTransactionCategory(new(request.Value), new UserId(_userContext.IdentityId));
            }
            else
            {
                return Result.Failure<TransactionCategory>(Error.InvalidRequest("Transaction category type is not supported"));
            }

            var isUnique = (await _transactionCategoryRepository.BrowseAllAsync()).All(tc => tc.Value != transactionCategory.Value);

            if (!isUnique)
            {
                return Result.Failure<TransactionCategory>(Error.InvalidRequest("Transaction category name must be unique"));
            }

            await _transactionCategoryRepository.AddAsync(transactionCategory, cancellationToken);

            var isSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return !isSuccess ? 
                Result.Failure<TransactionCategory>(Error.TaskFailed($"Problem while adding new transaction category with ID: {transactionCategory.Id} to database")) : 
                transactionCategory;
        }
    }
}