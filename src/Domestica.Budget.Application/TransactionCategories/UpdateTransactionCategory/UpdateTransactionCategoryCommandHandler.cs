using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionCategories;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionCategories.UpdateTransactionCategory
{
    internal sealed class UpdateTransactionCategoryCommandHandler : ICommandHandler<UpdateTransactionCategoryCommand, TransactionCategory>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;

        public UpdateTransactionCategoryCommandHandler(IUnitOfWork unitOfWork, ITransactionCategoryRepository transactionCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionCategoryRepository = transactionCategoryRepository;
        }

        public async Task<Result<TransactionCategory>> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var transactionCategoryToUpdate =
                await _transactionCategoryRepository.GetByIdAsync(new (Guid.Parse(request.TransactionCategoryId)), cancellationToken);

            if (transactionCategoryToUpdate is null)
            {
                return Result.Failure<TransactionCategory>(
                    Error.NotFound($"Transaction category with ID: {request.TransactionCategoryId} not found"));
            }

            transactionCategoryToUpdate.UpdateValue(new (request.NewValue));

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(transactionCategoryToUpdate);
            }

            return Result.Failure<TransactionCategory>(Error.TaskFailed($"Problem while updating transaction category with ID: {request.TransactionCategoryId}"));
        }
    }
}
