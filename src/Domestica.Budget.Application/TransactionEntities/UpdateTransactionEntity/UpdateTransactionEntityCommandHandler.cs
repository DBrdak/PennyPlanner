using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionEntities.UpdateTransactionEntity
{
    internal sealed class UpdateTransactionEntityCommandHandler : ICommandHandler<UpdateTransactionEntityCommand, TransactionEntity>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTransactionEntityCommandHandler(ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork)
        {
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionEntity>> Handle(UpdateTransactionEntityCommand request, CancellationToken cancellationToken)
        {
            var transactionEntity = await _transactionEntityRepository.GetByIdAsync(new (Guid.Parse(request.Id)), cancellationToken);

            if (transactionEntity is null)
            {
                return Result.Failure<TransactionEntity>(Error.NotFound("Transaction entity not found"));
            }

            transactionEntity.ChangeName(new(request.NewName));

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(transactionEntity);
            }

            return Result.Failure<TransactionEntity>(Error.TaskFailed($"Problem while updating transaction entity name with ID: {transactionEntity.Id}"));
        }
    }
}
