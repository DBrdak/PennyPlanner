using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Money.DB;
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
            Account? newAccount;

            switch (request.NewAccountData.Type)
            {
                case "Savings":
                    newAccount = CreateAccount<SavingsAccount>(request.NewAccountData);

                    break;
                case "Transactional":
                    newAccount = CreateAccount<TransactionalAccount>(request.NewAccountData);
                    break;
                default:
                    return Result.Failure<Account>(Error.InvalidRequest("Account type not supported"));
            }

            if (newAccount is null)
            {
                return Result.Failure<Account>(Error.InvalidRequest("Account type not supported"));
            }

            await _accountRepository.AddAsync(newAccount, cancellationToken);
            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if(isSuccessful)
            {
                return Result.Success(newAccount);
            }

            return Result.Failure<Account>(Error.TaskFailed("Problem while saving new account to database"));
        }

        private TAccount? CreateAccount<TAccount>(NewAccountData newAccountData) where TAccount : class
        {//TODO fetch currency from user
            return Activator.CreateInstance(
                typeof(TAccount),
                new AccountName(newAccountData.Name),
                Currency.Usd, //TODO Fetch currency from user
                newAccountData.InitialBalance) as TAccount;
        }
    }
}
