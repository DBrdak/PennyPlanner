using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionCategories;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionCategories.AddTransactionCategory
{
    internal sealed class AddTransactionCategoryCommandHandler : ICommandHandler<AddTransactionCategoryCommand, TransactionCategory>
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTransactionCategoryCommandHandler(ITransactionCategoryRepository transactionCategoryRepository, IUnitOfWork unitOfWork)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionCategory>> Handle(AddTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            TransactionCategory transactionCategory;

            if (string.Equals(request.Type, TransactionCategoryType.Income.Value, StringComparison.CurrentCultureIgnoreCase))
            {
                transactionCategory = new IncomeTransactionCategory(new(request.Value));
            }
            else if (string.Equals(request.Type, TransactionCategoryType.Outcome.Value, StringComparison.CurrentCultureIgnoreCase))
            {
                transactionCategory = new OutcomeTransactionCategory(new(request.Value));
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
