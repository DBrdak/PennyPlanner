using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Responses.DB;

namespace Domestica.Budget.Application.Accounts.GetAccounts
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
            //TODO Retrive user id
            var accounts = await _accountRepository.BrowseAccounts(cancellationToken);

            return accounts.Select(AccountModel.FromDomainObject).ToList();
        }
    }
}
