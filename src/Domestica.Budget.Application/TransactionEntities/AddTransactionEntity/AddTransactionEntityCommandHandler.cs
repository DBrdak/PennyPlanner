using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
{
    internal sealed class AddTransactionEntityCommandHandler : ICommandHandler<AddTransactionEntityCommand>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTransactionEntityCommandHandler(ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork)
        {
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTransactionEntityCommand request, CancellationToken cancellationToken)
        {
            switch (request.Type.Value)
            {
                case "Recipient":
                    var recipient = new TransactionRecipient(request.Name);
                    await _transactionEntityRepository.AddAsync(recipient, cancellationToken);
                    break;
                case "Sender":
                    var sender = new TransactionSender(request.Name);
                    await _transactionEntityRepository.AddAsync(sender, cancellationToken);
                    break;
                default: return Result.Failure(Error.InvalidRequest("Invalid transaction entity type"));
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success();
            }

            return Result.Failure(Error.TaskFailed("Problem while adding new transaction entity"));
        }
    }
}
