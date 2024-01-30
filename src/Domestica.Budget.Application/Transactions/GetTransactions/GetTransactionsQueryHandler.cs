using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.GetTransactions
{
    internal sealed class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionsQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<List<TransactionDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.BrowseUserTransactions(cancellationToken);

            return transactions.Select(TransactionDto.FromDomainObject).ToList();
        }
    }
}
