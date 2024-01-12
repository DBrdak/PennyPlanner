using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Responses.DB;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    internal sealed class AddAccountCommandHandler : ICommandHandler<AddAccountCommand, Account> 
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Account>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            Account newAccount;

            switch (request.NewAccountData.Type.Value)
            {
                case "Savings":
                    newAccount = CreateSavingsAccount(request.NewAccountData);
                    await _accountRepository.AddAsync(newAccount, cancellationToken);
                    break;
                case "Transactional":
                    newAccount = CreateTransactionalAccount(request.NewAccountData);
                    await _accountRepository.AddAsync(newAccount, cancellationToken);
                    break;
                default:
                    return Result.Failure<Account>(Error.InvalidRequest("Account type not supported"));
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if(isSuccessful)
            {
                return Result.Success(newAccount);
            }

            return Result.Failure<Account>(Error.TaskFailed("Problem while saving new account to database"));
        }

        private TransactionalAccount CreateTransactionalAccount(NewAccountData newTransactionalAccountData)
        {
            return new TransactionalAccount(
                newTransactionalAccountData.Name,
                newTransactionalAccountData.InitialBalance.Currency,
                newTransactionalAccountData.InitialBalance.Amount);
        }

        private SavingsAccount CreateSavingsAccount(NewAccountData newSavingsAccountData)
        {
            return new SavingsAccount(
                newSavingsAccountData.Name,
                newSavingsAccountData.InitialBalance.Currency,
                newSavingsAccountData.InitialBalance.Amount);
        }
    }
}
