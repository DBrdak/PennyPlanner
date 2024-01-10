using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Responses.DB;

namespace Domestica.Budget.Application.Accounts.AddAccount
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
                default:
                    return Result.Failure(Error.InvalidRequest("Account type not supported"));
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
