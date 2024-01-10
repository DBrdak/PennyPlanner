using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    internal sealed class AddInternalTransactionCommandHandler : ICommandHandler<AddInternalTransactionCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddInternalTransactionCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _accountRepository.GetUserAccountByIdAsync(request.FromAccountId, cancellationToken);

            if (fromAccount is null)
            {
                return Result.Failure(Error.NotFound($"Account with ID: {request.FromAccountId} not found"));
            }

            var toAccount = await _accountRepository.GetUserAccountByIdAsync(request.ToAccountId, cancellationToken);

            if (toAccount is null)
            {
                return Result.Failure(Error.NotFound($"Account with ID: {request.ToAccountId} not found"));
            }

            TransactionService.CreateInternalTransaction(request.TransactionAmount, fromAccount, toAccount);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success();
            }

            return Result.Failure(Error.TaskFailed("Problem while adding internal transaction"));
        }
    }
}
