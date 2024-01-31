using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Transactions;
using Money.DB;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    internal sealed class AddInternalTransactionCommandHandler : ICommandHandler<AddInternalTransactionCommand, Transaction[]>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddInternalTransactionCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Transaction[]>> Handle(AddInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _accountRepository.GetUserAccountByIdAsync(new(Guid.Parse(request.FromAccountId)), cancellationToken);

            if (fromAccount is null)
            {
                return Result.Failure<Transaction[]>(Error.NotFound($"Account with ID: {request.FromAccountId} not found"));
            }

            var toAccount = await _accountRepository.GetUserAccountByIdAsync(new(Guid.Parse(request.ToAccountId)), cancellationToken);

            if (toAccount is null)
            {
                return Result.Failure<Transaction[]>(Error.NotFound($"Account with ID: {request.ToAccountId} not found"));
            }
            // TODO fetch currency from user
            var currency = Currency.Usd;

            var createdTransactions = TransactionService.CreateInternalTransaction(
                new(request.TransactionAmount, currency),
                fromAccount,
                toAccount);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(createdTransactions);
            }

            return Result.Failure<Transaction[]>(Error.TaskFailed("Problem while adding internal transaction"));
        }
    }
}
