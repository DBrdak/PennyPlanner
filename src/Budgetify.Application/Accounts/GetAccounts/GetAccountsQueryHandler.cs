using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace Budgetify.Application.Accounts.GetAccounts
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
