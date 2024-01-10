using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    internal sealed class AddOutcomeTransactionCommandHandler : ICommandHandler<AddOutcomeTransactionCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddOutcomeTransactionCommandHandler(IAccountRepository accountRepository, ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddOutcomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var sourceAccount = await _accountRepository.GetUserAccountByIdAsync(request.SourceAccountId, cancellationToken);

            if (sourceAccount is null)
            {
                return Result.Failure(Error.NotFound($"Account with ID: {request.SourceAccountId} not found"));
            }

            var recipient = await _transactionEntityRepository.GetByIdAsync(request.RecipientId, cancellationToken) as TransactionRecipient;

            if (recipient is null)
            {
                return Result.Failure(Error.NotFound($"Recipient with ID: {request.RecipientId} not found"));
            }

            TransactionService.CreateOutgoingTransaction(request.TransactionAmount, sourceAccount, recipient, request.Category);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success();
            }

            return Result.Failure(Error.TaskFailed("Problem while adding outcome transaction"));
        }
    }
}
