using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.TransactionEntities;
using PennyPlanner.Domain.TransactionEntities.TransactionRecipients;
using PennyPlanner.Domain.TransactionEntities.TransactionSenders;
using Responses.DB;

namespace PennyPlanner.Application.TransactionEntities.AddTransactionEntity
{
    internal sealed class AddTransactionEntityCommandHandler : ICommandHandler<AddTransactionEntityCommand, TransactionEntity>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public AddTransactionEntityCommandHandler(ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<TransactionEntity>> Handle(AddTransactionEntityCommand request, CancellationToken cancellationToken)
        {
            var isNameUnique = (await _transactionEntityRepository.BrowseUserTransactionEntitiesAsync()).All(te => te.Name.Value.ToLower() != request.Name.ToLower());

            if (!isNameUnique)
            {
                return Result.Failure<TransactionEntity>(Error.InvalidRequest($"Transaction entity with name {request.Name} already exist"));
            }

            TransactionEntity newTransactionEntity;

            if (string.Equals(request.Type, TransactionEntityType.Recipient.Value, StringComparison.CurrentCultureIgnoreCase))
            {
                newTransactionEntity = new TransactionRecipient(new(request.Name), new(_userContext.IdentityId));
            }
            else if (string.Equals(request.Type, TransactionEntityType.Sender.Value, StringComparison.CurrentCultureIgnoreCase))
            {
                newTransactionEntity = new TransactionSender(new(request.Name), new(_userContext.IdentityId));
            }
            else
            {
                return Result.Failure<TransactionEntity>(Error.InvalidRequest("Invalid transaction entity type"));
            }

            await _transactionEntityRepository.AddAsync(newTransactionEntity, cancellationToken);
            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(newTransactionEntity);
            }

            return Result.Failure<TransactionEntity>(Error.TaskFailed("Problem while adding new transaction entity"));
        }
    }
}
