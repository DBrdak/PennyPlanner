using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace Budgetify.Application.Accounts.UpdateAccount
{
    internal sealed class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var account = await _accountRepository.GetUserAccountByIdAsync(request.AccountUpdateData.AccountId, cancellationToken);

            if (account is null)
            {
                return Result.Failure(Error.NotFound("Account with Id not found"));
            }

            account.UpdateAccount(request.AccountUpdateData.Name, request.AccountUpdateData.Balance);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success();
            }

            return Result.Failure(Error.TaskFailed($"Problem while updating account with ID: {account.Id}"));
        }
    }
}
