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
            var isUniqueName = (await _accountRepository.BrowseAccounts()).All(a => a.Name.Value.ToLower() != request.NewAccountData.Name.ToLower());

            if (!isUniqueName)
            {
                return Result.Failure<Account>(Error.InvalidRequest($"Account with name {request.NewAccountData.Name} already exist"));
            }

            Account? newAccount;

            if (request.NewAccountData.Type == AccountType.Savings.Value)
            {
                newAccount = CreateAccount<SavingsAccount>(request.NewAccountData);
            }
            else if (request.NewAccountData.Type == AccountType.Transactional.Value)
            {
                newAccount = CreateAccount<TransactionalAccount>(request.NewAccountData);
            }
            else
            {
                return Result.Failure<Account>(Error.InvalidRequest("Account type not supported"));
            }

            if (newAccount is null)
            {
                return Result.Failure<Account>(Error.InvalidRequest("Problem while creating new account"));
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
