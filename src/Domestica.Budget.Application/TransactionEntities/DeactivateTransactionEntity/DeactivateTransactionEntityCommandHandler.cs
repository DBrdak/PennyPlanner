using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionEntities.DeactivateTransactionEntity
{
    internal sealed class DeactivateTransactionEntityCommandHandler : ICommandHandler<DeactivateTransactionEntityCommand, TransactionEntity>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateTransactionEntityCommandHandler(ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork)
        {
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionEntity>> Handle(DeactivateTransactionEntityCommand request, CancellationToken cancellationToken)
        {
            var transactionEntity = await _transactionEntityRepository.GetByIdAsync(
                request.TransactionEntityId,
                cancellationToken);

            if (transactionEntity is null)
            {
                return Result.Failure<TransactionEntity>(Error.NotFound($"Transaction entity with ID: {request.TransactionEntityId} not found"));
            }

            transactionEntity.Deactivate();

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(transactionEntity);
            }

            return Result.Failure<TransactionEntity>(Error.TaskFailed($"Problem while deactivating transaction entity with ID: {transactionEntity.Id}"));
        }
    }
}
