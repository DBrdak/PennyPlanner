﻿using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Responses.DB;

namespace Domestica.Budget.Application.Accounts.UpdateAccount
{
    internal sealed class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Account>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var account = await _accountRepository.GetUserAccountByIdAsync(request.AccountUpdateData.AccountId, cancellationToken);

            if (account is null)
            {
                return Result.Failure<Account>(Error.NotFound("Account with Id not found"));
            }

            account.UpdateAccount(request.AccountUpdateData.Name, request.AccountUpdateData.Balance);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(account);
            }

            return Result.Failure<Account>(Error.TaskFailed($"Problem while updating account with ID: {account.Id}"));
        }
    }
}
