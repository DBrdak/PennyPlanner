using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    internal sealed class AddIncomeTransactionCommandHandler : ICommandHandler<AddIncomeTransactionCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddIncomeTransactionCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, ITransactionEntityRepository transactionEntityRepository)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _transactionEntityRepository = transactionEntityRepository;
        }

        public async Task<Result> Handle(AddIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var destinationAccount = await _accountRepository.GetUserAccountByIdAsync(request.DestinationAccountId, cancellationToken);

            if (destinationAccount is null)
            {
                return Result.Failure(Error.NotFound($"Account with ID: {request.DestinationAccountId} not found"));
            }

            var sender = await _transactionEntityRepository.GetByIdAsync(request.SenderId, cancellationToken) as TransactionSender;

            if (sender is null)
            {
                return Result.Failure(Error.NotFound($"Sender with ID: {request.SenderId} not found"));
            }

            TransactionService.CreateIncomingTransaction(request.TransactionAmount, destinationAccount, sender, request.Category);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success();
            }

            return Result.Failure(Error.TaskFailed("Problem while adding income transaction"));
        }
    }
}
