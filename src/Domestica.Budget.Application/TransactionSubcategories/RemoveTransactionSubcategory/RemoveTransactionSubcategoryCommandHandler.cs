using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionSubcategories;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionSubcategories.RemoveTransactionSubcategory
{
    internal sealed class RemoveTransactionSubcategoryCommandHandler : ICommandHandler<RemoveTransactionSubcategoryCommand, TransactionSubcategory>
    {
        private readonly ITransactionSubcategoryRepository _transactionSubcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTransactionSubcategoryCommandHandler(ITransactionSubcategoryRepository transactionSubcategoryRepository, IUnitOfWork unitOfWork)
        {
            _transactionSubcategoryRepository = transactionSubcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionSubcategory>> Handle(RemoveTransactionSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var transactionSubcategoryToDelete = await _transactionSubcategoryRepository.GetByIdAsync(
                new(Guid.Parse(request.TransactionSubcategoryId)),
                cancellationToken);

            if (transactionSubcategoryToDelete is null)
            {
                return Result.Failure<TransactionSubcategory>(
                    Error.NotFound($"Transaction subcategory with ID: {request.TransactionSubcategoryId} mot found"));
            }

            _transactionSubcategoryRepository.Remove(transactionSubcategoryToDelete);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return transactionSubcategoryToDelete;
            }

            return Result.Failure<TransactionSubcategory>(Error.TaskFailed($"Problem while removing transaction subcategory with ID: {request.TransactionSubcategoryId}"));

        }
    }
}
