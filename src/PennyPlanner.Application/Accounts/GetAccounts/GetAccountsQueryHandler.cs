using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Accounts;
using Responses.DB;

namespace PennyPlanner.Application.Accounts.GetAccounts
{
    internal sealed class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, List<AccountModel>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<List<AccountModel>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.BrowseAccounts(cancellationToken);

            return accounts.Select(AccountModel.FromDomainObject).ToList();
        }
    }
}
