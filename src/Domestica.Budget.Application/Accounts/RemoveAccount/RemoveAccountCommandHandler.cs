using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Responses.DB;

namespace Domestica.Budget.Application.Accounts.RemoveAccount
{
    internal sealed class RemoveAccountCommandHandler : ICommandHandler<RemoveAccountCommand, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Account>> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetUserAccountByIdAsync(request.AccountId, cancellationToken);

            if (account is null)
            {
                return Result.Failure<Account>(Error.NotFound($"Account with ID: {request.AccountId} not found"));
            }

            _accountRepository.Remove(account);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(account);
            }

            return Result.Failure<Account>(Error.TaskFailed($"Problem while removing account with ID: {account.Id}"));
        }
    }
}
