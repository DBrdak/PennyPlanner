using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Responses.DB;

namespace Domestica.Budget.Application.Accounts.DeactivateAccount
{
    internal sealed class DeactivateAccountCommandHandler : ICommandHandler<DeactivateAccountCommand, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Account>> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetUserAccountByIdAsync(request.AccountId, cancellationToken);

            if (account is null)
            {
                return Result.Failure<Account>(Error.NotFound($"Account with ID: {request.AccountId} not found"));
            }

            account.DeactivateAccount();

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(account);
            }

            return Result.Failure<Account>(Error.TaskFailed($"Problem while deactivating account with ID: {account.Id}"));
        }
    }
}
