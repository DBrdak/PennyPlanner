using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionSubcategories;
using Exceptions.DB;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionSubcategories.AddTransactionSubcategory
{
    internal sealed class AddTransactionSubcategoryCommandHandler : ICommandHandler<AddTransactionSubcategoryCommand, TransactionSubcategory>
    {
        private readonly ITransactionSubcategoryRepository _transactionSubcategoryRepository;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public AddTransactionSubcategoryCommandHandler(ITransactionSubcategoryRepository transactionSubcategoryRepository, IUnitOfWork unitOfWork, ITransactionCategoryRepository transactionCategoryRepository, IUserContext userContext)
        {
            _transactionSubcategoryRepository = transactionSubcategoryRepository;
            _unitOfWork = unitOfWork;
            _transactionCategoryRepository = transactionCategoryRepository;
            _userContext = userContext;
        }

        public async Task<Result<TransactionSubcategory>> Handle(AddTransactionSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _transactionCategoryRepository.GetByIdIncludeAsync(
                new(Guid.Parse(request.CategoryId)),
                c => c.Subcategories,
                cancellationToken);

            if (category is null)
            {
                return Result.Failure<TransactionSubcategory>(Error.NotFound($"Category with ID: {request.CategoryId} not found"));
            }

            var newTransactionSubcategory = new TransactionSubcategory(
                new TransactionSubcategoryValue(request.Value),
                category,
                new(_userContext.IdentityId));

            if (FailAddSubcategoryToCategory(request, category, newTransactionSubcategory, out var result))
            {
                return result!;
            }

            await _transactionSubcategoryRepository.AddAsync(newTransactionSubcategory, cancellationToken);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return isSuccessful ? 
                newTransactionSubcategory : 
                Result.Failure<TransactionSubcategory>(Error.TaskFailed($"Problem while adding new transaction subcategory with ID: {newTransactionSubcategory.Id.Value}"));
        }

        private static bool FailAddSubcategoryToCategory(
            AddTransactionSubcategoryCommand request,
            TransactionCategory category,
            TransactionSubcategory newTransactionSubcategory,
            out Result<TransactionSubcategory>? result)
        {
            try
            {
                category.AddSubcategory(newTransactionSubcategory);
            }
            catch (DomainException)
            {
                result = Result.Failure<TransactionSubcategory>(
                    Error.InvalidRequest(
                        $"Transaction subcategory: {request.Value} already exist in category with ID: {category.Id.Value}"));
                return true;
            }

            result = default;
            return false;
        }
    }
}
