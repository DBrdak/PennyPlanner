using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionCategories.DeleteTransactionCategory
{
    internal sealed class DeleteTransactionCategoryCommandHandler : ICommandHandler<DeleteTransactionCategoryCommand, TransactionCategory>
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransactionCategoryCommandHandler(ITransactionCategoryRepository transactionCategoryRepository, IUnitOfWork unitOfWork, ITransactionRepository transactionRepository, IBudgetPlanRepository budgetPlanRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
            _budgetPlanRepository = budgetPlanRepository;
        }
        
        public async Task<Result<TransactionCategory>> Handle(DeleteTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var transactionCategory = await _transactionCategoryRepository.GetByIdAsync(
                new(Guid.Parse(request.TransactionCategoryId)),
                cancellationToken);

            if (transactionCategory is null)
            {
                return Result.Failure<TransactionCategory>(
                    Error.NotFound($"Transaction category with ID: {request.TransactionCategoryId} not found"));
            }

            if (await IsCategoryUsed(transactionCategory.Id, cancellationToken))
            {
                return Result.Failure<TransactionCategory>(
                    Error.InvalidRequest(
                        "Transaction category cannot be deleted, because it is related to other entities"));
            }

            _transactionCategoryRepository.Remove(transactionCategory);

            var isSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return isSuccess ?
                    transactionCategory :
                    Result.Failure<TransactionCategory>(Error.TaskFailed($"Problem while removing transaction category with ID: {transactionCategory.Id}"));
        }

        private async Task<bool> IsCategoryUsed(TransactionCategoryId id, CancellationToken cancellationToken)
        {
            var isCategoryUsedByBudgetPlans = (await _budgetPlanRepository
                    .BrowseUserBudgetPlansAsync(cancellationToken))
                        .SelectMany(bp => bp.BudgetedTransactionCategories)
                            .Any(bpc => bpc.CategoryId == id);

            return isCategoryUsedByBudgetPlans;
        }
    }
}
