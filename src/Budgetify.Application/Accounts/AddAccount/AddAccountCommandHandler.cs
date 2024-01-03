using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Budgetify.Domain.Accounts.SavingsAccounts;
using Budgetify.Domain.Accounts.TransactionalAccounts;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace Budgetify.Application.Accounts.AddAccount
{
    internal sealed class AddAccountCommandHandler : ICommandHandler<AddAccountCommand> 
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            switch (request.NewAccountData.Type.Value)
            {
                case "Savings":
                    await CreateSavingsAccount(request.NewAccountData);
                    break;
                case "Transactional":
                    await CreateTransactionalAccount(request.NewAccountData);
                    break;
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if(isSuccessful)
            {
                return Result.Success();
            }

            return Result.Failure(Error.TaskFailed("Problem while saving new account to database"));
        }

        private async Task CreateTransactionalAccount(NewAccountData newTransactionalAccountData)
        {
            var newTransactionalAccount = new TransactionalAccount(
                newTransactionalAccountData.Name,
                newTransactionalAccountData.Currency,
                newTransactionalAccountData.InitialBalance);

            await _accountRepository.AddAsync(newTransactionalAccount);
        }

        private async Task CreateSavingsAccount(NewAccountData newSavingsAccountData)
        {
            var newSavingsAccount = new SavingsAccount(
                newSavingsAccountData.Name,
                newSavingsAccountData.Currency,
                newSavingsAccountData.InitialBalance);

            await _accountRepository.AddAsync(newSavingsAccount);
        }
    }
}
