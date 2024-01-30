using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    internal sealed class AddOutcomeTransactionCommandHandler : ICommandHandler<AddOutcomeTransactionCommand, Transaction>
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

        public async Task<Result<Transaction>> Handle(AddOutcomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var sourceAccount = await _accountRepository.GetUserAccountByIdAsync(new(Guid.Parse(request.SourceAccountId)), cancellationToken);

            if (sourceAccount is null)
            {
                return Result.Failure<Transaction>(Error.NotFound($"Account with ID: {request.SourceAccountId} not found"));
            }

            var recipient = await _transactionEntityRepository.GetByIdIncludeAsync(
                new(Guid.Parse(request.RecipientId)),
                te => te.Transactions,
                cancellationToken) as TransactionRecipient;

            if (recipient is null)
            {
                return Result.Failure<Transaction>(Error.NotFound($"Recipient with ID: {request.RecipientId} not found"));
            }

            var createdTransaction = TransactionService.CreateOutgoingTransaction(
                request.TransactionAmount.ToDomainObject(),
                sourceAccount,
                recipient,
                OutgoingTransactionCategory.FromValue(request.Category));

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(createdTransaction);
            }

            return Result.Failure<Transaction>(Error.TaskFailed("Problem while adding outcome transaction"));
        }
    }
}
