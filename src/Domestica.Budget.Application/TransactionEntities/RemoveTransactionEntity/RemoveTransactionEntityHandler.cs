using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionEntities.RemoveTransactionEntity
{
    internal sealed class RemoveTransactionEntityHandler : ICommandHandler<RemoveTransactionEntityCommand, TransactionEntity>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTransactionEntityHandler(ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork)
        {
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionEntity>> Handle(RemoveTransactionEntityCommand request, CancellationToken cancellationToken)
        {
            var transactionEntity = await _transactionEntityRepository.GetByIdAsync(
                new (Guid.Parse(request.TransactionEntityId)),
                cancellationToken);

            if (transactionEntity is null)
            {
                return Result.Failure<TransactionEntity>(Error.NotFound($"Transaction entity with ID: {request.TransactionEntityId} not found"));
            }

            _transactionEntityRepository.Remove(transactionEntity);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(transactionEntity);
            }

            return Result.Failure<TransactionEntity>(Error.TaskFailed($"Problem while removing transaction entity with ID: {transactionEntity.Id}"));
        }
    }
}
