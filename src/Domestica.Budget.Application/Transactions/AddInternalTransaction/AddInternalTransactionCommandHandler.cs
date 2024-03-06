using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
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
        private readonly IUserContext _userContext;

        public AddInternalTransactionCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<Transaction[]>> Handle(AddInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _accountRepository.GetAccountByIdAsync(new(Guid.Parse(request.FromAccountId)), cancellationToken);

            if (fromAccount is null)
            {
                return Result.Failure<Transaction[]>(Error.NotFound($"Account with ID: {request.FromAccountId} not found"));
            }

            var toAccount = await _accountRepository.GetAccountByIdAsync(new(Guid.Parse(request.ToAccountId)), cancellationToken);

            if (toAccount is null)
            {
                return Result.Failure<Transaction[]>(Error.NotFound($"Account with ID: {request.ToAccountId} not found"));
            }

            var currency = Currency.FromCode(_userContext.UserCurrencyCode);

            var createdTransactions = TransactionService.CreateInternalTransaction(
                new(request.TransactionAmount, currency),
                fromAccount,
                toAccount,
                request.TransactionDateTime);

            

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(createdTransactions);
            }

            return Result.Failure<Transaction[]>(Error.TaskFailed("Problem while adding internal transaction"));
        }
    }
}
