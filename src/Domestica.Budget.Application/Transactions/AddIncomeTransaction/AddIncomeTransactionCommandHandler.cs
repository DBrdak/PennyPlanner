using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions;
using Money.DB;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    internal sealed class AddIncomeTransactionCommandHandler : ICommandHandler<AddIncomeTransactionCommand, Transaction>
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

        public async Task<Result<Transaction>> Handle(AddIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var destinationAccount = await _accountRepository.GetUserAccountByIdAsync(new(Guid.Parse(request.DestinationAccountId)), cancellationToken);

            if (destinationAccount is null)
            {
                return Result.Failure<Transaction>(Error.NotFound($"Account with ID: {request.DestinationAccountId} not found"));
            }

            var sender = await _transactionEntityRepository.GetByIdIncludeAsync(
                new(Guid.Parse(request.SenderId)),
                te => te.Transactions,
                cancellationToken) as TransactionSender;

            if (sender is null)
            {
                return Result.Failure<Transaction>(Error.NotFound($"Sender with ID: {request.SenderId} not found"));
            }

            // TODO fetch currency from user
            var currency = Currency.Usd;

            var createdTransaction = TransactionService.CreateIncomingTransaction(
                new (request.TransactionAmount, currency),
                destinationAccount,
                sender,
                IncomingTransactionCategory.FromValue(request.Category),
                request.TransactionDateTime);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(createdTransaction);
            }

            return Result.Failure<Transaction>(Error.TaskFailed("Problem while adding income transaction"));
        }
    }
}
