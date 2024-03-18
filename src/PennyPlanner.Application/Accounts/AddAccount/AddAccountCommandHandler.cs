using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Money.DB;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Application.Settings.ValidationSettings;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.Accounts.SavingsAccounts;
using PennyPlanner.Domain.Accounts.TransactionalAccounts;
using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.Accounts.AddAccount
{
    internal sealed class AddAccountCommandHandler : ICommandHandler<AddAccountCommand, Account> 
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public AddAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<Account>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var validationFailure = await ValidateAsync(request, cancellationToken);

            if (validationFailure is not null)
            {
                return validationFailure;
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

        private async Task<Result<Account>?> ValidateAsync(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var isUniqueName = (await _accountRepository.BrowseAccounts()).All(a => a.Name.Value.ToLower() != request.NewAccountData.Name.ToLower());

            if (!isUniqueName)
            {
                return Result.Failure<Account>(Error.InvalidRequest($"Account with name {request.NewAccountData.Name} already exist"));
            }

            var isAccountLimitExceeded = (await _accountRepository.BrowseAccounts()).Count >= AccountValidationSettings.AccountsLimit;

            if (isAccountLimitExceeded)
            {
                return Result.Failure<Account>(
                    Error.InvalidRequest(
                        $"You've reached maximum number of accounts"));
            }

            return null;
        }

        private TAccount? CreateAccount<TAccount>(NewAccountData newAccountData) where TAccount : class
        {
            return Activator.CreateInstance(
                typeof(TAccount),
                new AccountName(newAccountData.Name),
                Currency.FromCode(_userContext.UserCurrencyCode),
                new UserId(Guid.Parse(_userContext.IdentityId)),
                newAccountData.InitialBalance) as TAccount;
        }
    }
}
