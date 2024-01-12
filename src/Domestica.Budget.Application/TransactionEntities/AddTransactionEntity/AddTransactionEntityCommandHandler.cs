﻿using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
{
    internal sealed class AddTransactionEntityCommandHandler : ICommandHandler<AddTransactionEntityCommand, TransactionEntity>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTransactionEntityCommandHandler(ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork)
        {
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionEntity>> Handle(AddTransactionEntityCommand request, CancellationToken cancellationToken)
        {
            TransactionEntity newTransactionEntity;

            switch (request.Type.Value)
            {
                case "Recipient":
                    newTransactionEntity = new TransactionRecipient(request.Name);
                    await _transactionEntityRepository.AddAsync(newTransactionEntity, cancellationToken);
                    break;
                case "Sender":
                    newTransactionEntity = new TransactionSender(request.Name);
                    await _transactionEntityRepository.AddAsync(newTransactionEntity, cancellationToken);
                    break;
                default:
                    return Result.Failure<TransactionEntity>(Error.InvalidRequest("Invalid transaction entity type"));
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(newTransactionEntity);
            }

            return Result.Failure<TransactionEntity>(Error.TaskFailed("Problem while adding new transaction entity"));
        }
    }
}
