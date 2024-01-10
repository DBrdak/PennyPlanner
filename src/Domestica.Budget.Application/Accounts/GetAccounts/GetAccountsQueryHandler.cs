using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Accounts;
using Responses.DB;

namespace Domestica.Budget.Application.Accounts.GetAccounts
{
    internal sealed class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, List<Account>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<List<Account>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id
            return await _accountRepository.BrowseUserAccounts(cancellationToken);
        }
    }
}
