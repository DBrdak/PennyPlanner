using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.RemoveTransaction
{
    internal sealed class RemoveTransactionCommandHandler : ICommandHandler<RemoveTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTransactionCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Transaction>> Handle(RemoveTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(new(Guid.Parse(request.TransactionId)), cancellationToken);

            if (transaction is null)
            {
                return Result.Failure<Transaction>(Error.NotFound($"Transaction with ID: {request.TransactionId} not found"));
            }

            _transactionRepository.Remove(transaction);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(transaction);
            }

            return Result.Failure<Transaction>(Error.TaskFailed($"Problem while removing transaction with ID: {transaction.Id.Value}"));
        }
    }
}
